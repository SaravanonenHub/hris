using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.EntriesDtos;
using API.Dtos.MasterDtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Entities.Actions;
using Core.Entities.Employees;
using Core.Entities.Entries;
using Core.Entities.Masters;
using Core.Entities.Notify;
using Core.Interfaces;
using Core.Interfaces.IActions;
using Core.Interfaces.IEntries;
using Core.Interfaces.IMaster;
using Core.Interfaces.ISignalR;
using Core.Specifications.EntriesSpec;
using Core.Specifications.MasterSpec;
using Infrastructure.Data;
using Infrastructure.Data.Services.Notify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
namespace API.Controllers.Entries
{

    public class LeaveController : BaseApiController
    {
        private readonly ILeaveService _service;
        private readonly IRequestService _requestService;
        private readonly INotificationService _notifyservice;
        private readonly IActionService<ActionHistory> _actionService;
        private readonly IEmployeeRepository _empservice;
        private readonly ILeavePolicyRepo _policyService;
        private readonly ITeamRepository _teamService;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
        private readonly IMapper _mapper;

        public LeaveController(ILeaveService service
        , IMapper mapper, IEmployeeRepository empservice
        , IHubContext<BroadcastHub, IHubClient> hubContext
        , INotificationService notifyservice, IActionService<ActionHistory> actionService, ILeavePolicyRepo policyService, IUnitOfWork unitOfWork, ITeamRepository teamService, IRequestService requestService)
        {
            _service = service;
            _mapper = mapper;
            _empservice = empservice;
            _hubContext = hubContext;
            _notifyservice = notifyservice;
            _actionService = actionService;
            _policyService = policyService;
            _teamService = teamService;
            _requestService = requestService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<LeaveResponseDto>> LeaveSubmit([FromForm] LeaveRequestDto leave)
        {
            if (ModelState.IsValid)
            {
                var _emp = await _empservice.GetEmployeeById(0, leave.EmployeeId);
                var _template = await _service.GetTemplatebyId(leave.TemplateId);
                DateTime fdate = DateTime.MinValue;
                DateTime tdate = DateTime.MinValue;
                if (!DateTime.TryParseExact(leave.FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fdate) && fdate == DateTime.MinValue)
                 return BadRequest(new ApiResponse(400, "Invalid From date!"));
                if (!DateTime.TryParseExact(leave.ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tdate) && tdate == DateTime.MinValue)
                   return BadRequest(new ApiResponse(400, "Invalid To date!"));
                leave.FromDate = fdate.ToString();
                leave.ToDate = tdate.ToString();
                var alreadyExist = await _service.AlreadyExists(_emp.Id, fdate, tdate);

                if (!alreadyExist) return BadRequest(new ApiResponse(400, "Leave already exist!"));

                var _leave = _mapper.Map<LeaveRequestDto, Leave>(leave);
                //_leave.Employee = _emp;
                int numericLength = 10;
                var _req = new Request
                {
                    Employee = _emp,
                    //Status = RequestAction.Submitted,
                    //CancellationStatus = 'N',
                    CurrentState = ActionTaken.Created,
                    Type = _template,
                    RequestedBy = _emp.EmployeeCode,
                    Description = $"Leave request raised on date from {leave.FromDate} to {leave.ToDate}",
                    RequestId = ""

                };
                var reqResult = await _requestService.CreateRequest(_req);
                if (reqResult == null) return BadRequest(new ApiResponse(400, "Problem creating Request"));
                _leave.Request = reqResult;
                var result = await _service.SubmitLeave(_leave);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem creating Leave"));
                var action = new ActionHistory
                {
                    Action = ActionTaken.Created,
                    ActionBy = result.Request.Employee.EmployeeCode,
                    Request = reqResult,
                    //Reason = "Submit Request"
                };
                var actionResult = await _actionService.CreateAction(action);
                NotifyProps notify = new NotifyProps
                {
                    Type = "Request",
                    Message = "New Leave approval Request",
                    // Team = _emp.Team,
                    // TeamRole = _emp.TeamRole,
                    Employee = null

                };
                var notifyResult = await _notifyservice.AddNotification(notify);
                await _hubContext.Clients.All.BrodcastMessage(notify);
                return Ok(_mapper.Map<Leave, LeaveResponseDto>(_leave));
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Model Invalid!"));
            }
         }
        
        [HttpGet("request/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(LeaveResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LeaveRequestById(int id)
        {
            //var param = new RequestSpecParams() { RequestId = id };
            var spec = new RequestsByTeamSpecification(id);
            var requests = await _service.GetRequestById(spec);
            if (requests == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Leave, LeaveResponseDto>(requests));

        }
       
        [HttpGet("entitlement")]
        public async Task<ActionResult<LeaveEntitlement>> EmployeeEntitlement([FromQuery] LeavePolicyFilterParams filter)
        {
            //first take corresponding person leave policy id from employee service
            int employeeId = filter.EmpId.HasValue ? filter.EmpId.Value : 0;
            if (!filter.EmpId.HasValue) return BadRequest(new ApiResponse(400, "Employee Not fount"));

            var _empNoTrack = _empservice.GetEmployeeByIdNoTrack(filter.EmpId.Value);
            var _emp = _empNoTrack.AsEnumerable().FirstOrDefault();
            //var _emp = await _empservice.GetEmployeeById(filter.EmpId.Value,filter.EmpCode);

            if (_emp == null) return BadRequest(new ApiResponse(400, "Employee Not fount"));
            //filter.Id = _emp.LeavePolicyId;

            var policySpec = new LeavePolicySpec(_emp.LeavePolicyId, filter);
            var _policy = await _policyService.GetLeavePolicyByIdWithFilter(policySpec);
            if(_policy == null) return BadRequest(new ApiResponse(400, "Policy Not found"));

            //from that leave policy we can get leave type and provided days


            //get all leave taken by corresponding person in this academic year
            var param = new RequestSpecParams { EmpId = filter.EmpId };
            var spec = new RequestsByTeamSpecification(param);
            var requests = await _service.MyLeaveRequests(spec);
            var enititlement = new LeaveEntitlement { Id = _policy.Id, PolicyName = _policy.PolicyName, ShortName = _policy.ShortName };
            //iterate over leave policy detail, and update leave taken property based of leaves received
            foreach(var detail in _policy.LeavePolicyDetails)
            {
                if(enititlement != null)
                {
                    var leaveType = _mapper.Map<LeaveType, LeaveTypeResponseDto>(detail.LeaveType);
                    var entDetail = new LeaveEntitlementDetail
                    {
                        LeaveType = leaveType
                        , Provided = detail.Day
                        , Taken = requests.Where(x => x.LeaveType == leaveType.ShortName && x.Status == RequestAction.Approved).Count()
                    };
                    enititlement.Details.Add(entDetail);
                }
            }    
            return Ok(enititlement);
        }

        //[HttpGet("pendingRequests/{empId}")]
        //public async Task<ActionResult<IReadOnlyList<Leave>>> LeavePendingRequest(int empId)
        //{
        //    //get employee teams
        //    var teamdetailFilter = new TeamDetailFilterSpec() { EmpId = empId };
        //    var teams = await _teamService.GetEmployeeTeams(teamdetailFilter);
        //    var _teamDetails = new List<Team>();
        //    if (teams?.Any() == true)
        //    {
        //        foreach (var team in teams)
        //        {
        //            var detail = await _teamService.GetTeamById(team.Team.Id);
        //            _teamDetails.Add(detail);
        //        }
        //    }


        //    //var _teamDetail = _mapper.Map<IReadOnlyList<TeamDetails>, IReadOnlyList<TeamDetailsResponseDto>>(teams);

        //    //get role mapped to his role
        //    var roleId = teams.Where(x => x.Employee.Id == empId).FirstOrDefault();
        //    var _rolesMapped = await _teamService.GetRolesMapped(roleId.Role.Id);

        //    //loop over team and get employees with having specific role
        //    var employees = _teamDetails
        //                    .SelectMany(x => x.TeamDetails)
        //                    .Where(emp => emp.Role.Role == _rolesMapped?.Select(x => x.ReportingRole.Role).FirstOrDefault())
        //                    .Select(emp => emp.Employee.Id).ToList();

        //    //List<int> employees = new List<int>();
        //    //if(teams != null)
        //    //{
        //    //    foreach(var team in _teamDetails)
        //    //    {
        //    //        if(team.TeamDetails != null)
        //    //        {
        //    //            foreach (var emp in team.TeamDetails)
        //    //            {
        //    //                if (emp.Role.Role == _rolesMapped.Select(x => x.ReportingRole.Role).FirstOrDefault())
        //    //                    employees.Add(emp.Employee.Id);
        //    //            }
        //    //        }

        //    //    }
        //    //}

        //    //
        //    var spec = new RequestsByTeamSpecification();
        //    var requests = await _service.MyLeaveRequests(spec);
        //    var employeesWithPendingLeave = requests.Where(x => employees.Contains(x.Request.Employee.Id));
        //    return Ok(employeesWithPendingLeave);
        //    //if (emp == null) return BadRequest("Employee not found");
        //    //var team = _empservice.GETE
        //    //get pending request of all employees with respect to team and role
        //}

        //[HttpGet("requests")]
        //public async Task<ActionResult<IReadOnlyList<Leave>>> LeaveRequests([FromQuery] RequestSpecParams requestParams)
        //{
        //    var spec = new RequestsByTeamSpecification(requestParams);
        //    var requests = await _service.MyLeaveRequests(spec);
        //    var data = _mapper.Map<IReadOnlyList<LeaveResponseDto>>(requests);
        //    return Ok(data);
        //}

        [HttpPut("approval")]
        public async Task<ActionResult<Leave>> LeaveAction([FromBody] RequestSpecParams model)
        {
            var spec = new RequestsByTeamSpecification(model);
            var leave = await _service.GetRequestById(spec);
            if (leave == null) return BadRequest(new ApiResponse(400, "Leave not found"));
            //var request = _service.GetRequestByIdNoTrack(spec).AsEnumerable().SingleOrDefault();
            var action = new ActionHistory
            {
                Action = ActionTaken.Closed,
                ActionBy = leave.Request.Employee.EmployeeCode,
                Request = leave.Request,
                Comment = "Action taken on Request"
            };
            var actionResult = await _actionService.CreateAction(action);
            if (actionResult == null) return BadRequest(new ApiResponse(400, "Problem updating action"));
            leave.Request.CurrentState = ActionTaken.Closed;
            var reqResult = await _requestService.UpdateRequest(leave.Request);
            if (reqResult == null) return BadRequest(new ApiResponse(400, "Problem updating Request"));
            switch (model.Status)
            {
                case "Approved":
                    leave.Status = RequestAction.Approved;
                    break;
                case "Rejected":
                    leave.Status = RequestAction.Rejected;
                    break;
                default:
                    break;
            };
            var result = await _service.UpdateLeave(leave);
            if (result == null) return BadRequest(new ApiResponse(400, "Problem updating Leave"));
            NotifyProps notify = new NotifyProps
            {
                Type = "Approval",
                Message = "New Leave approval Approved",
                // Team = request.Employee.Team,
                // TeamRole = request.Employee.TeamRole,
                Employee = null

            };
            var notifyResult = await _notifyservice.AddNotification(notify);
            await _hubContext.Clients.All.BrodcastMessage(notify);
            return Ok();
        }
        //[HttpPost("bulkapporval")]
        //public async Task<ActionResult<Leave>> BulkAction([FromQuery] string bulkIds)
        //{
        //    string[] requestIDs = bulkIds.Split(",");
        //    var spec = new RequestsByTeamSpecification(requestIDs);
        //    var requests = await _service.MyLeaveRequests(spec);
        //    if (requests != null)
        //    {
        //        foreach (var req in requests)
        //        {
        //            req.Request.Status = RequestAction.Approved;
        //            var result = await _service.UpdateLeave(req);
        //        }
        //    }
        //    //var data = _mapper.Map<IReadOnlyList<LeaveResponseDto>>(request);
        //    return Ok();
        //}

    }
}