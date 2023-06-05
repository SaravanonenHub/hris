using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.EntriesDtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Actions;
using Core.Entities.Entries;
using Core.Entities.Notify;
using Core.Interfaces;
using Core.Interfaces.IActions;
using Core.Interfaces.IEntries;
using Core.Interfaces.ISignalR;
using Core.Specifications.EntriesSpec;
using Infrastructure.Data.Services.Notify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
namespace API.Controllers.Entries
{

    public class LeaveController : BaseApiController
    {
        private readonly ILeaveService _service;
        private readonly INotificationService _notifyservice;
        private readonly IActionService<LeaveAction> _actionService;
        private readonly IEmployeeRepository _empservice;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
        private readonly IMapper _mapper;
        public LeaveController(ILeaveService service
        , IMapper mapper, IEmployeeRepository empservice
        , IHubContext<BroadcastHub, IHubClient> hubContext
        , INotificationService notifyservice, IActionService<LeaveAction> actionService)
        {
            _service = service;
            _mapper = mapper;
            _empservice = empservice;
            _hubContext = hubContext;
            _notifyservice = notifyservice;
            _actionService = actionService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<LeaveResponseDto>> LeaveSubmit([FromForm] LeaveRequestDto leave)
        {
            if (ModelState.IsValid)
            {
                var _emp = await _empservice.GetEmployeeById(0, leave.EmployeeId);
                var alreadyExist = await _service.AlreadyExists(_emp.Id, leave.FromDate, leave.ToDate);

                if (!alreadyExist) return BadRequest(new ApiResponse(400, "Leave already exist!"));

                var _leave = _mapper.Map<LeaveRequestDto, Leave>(leave);
                _leave.Employee = _emp;
                var result = await _service.SubmitLeave(_leave);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem creating Leave"));
                var action = new LeaveAction
                {
                    Action = ActionTaken.Ongoing,
                    ActionBy = result.Employee.EmployeeCode,
                    Leave = result,
                    Reason = "Submit Request"
                };
                var actionResult = await _actionService.CreateAction(action);
                NotifyProps notify = new NotifyProps
                {
                    Type = "Request",
                    Message = "New Leave approval Request",
                    Team = _emp.Team,
                    TeamRole = _emp.TeamRole,
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
        [HttpGet("requests")]
        public async Task<ActionResult<IReadOnlyList<Leave>>> LeaveRequests([FromQuery] RequestSpecParams requestParams)
        {
            var spec = new RequestsByTeamSpecification(requestParams);
            var requests = await _service.GetReqForApproval(spec);
            var data = _mapper.Map<IReadOnlyList<LeaveResponseDto>>(requests);
            return Ok(data);
        }
        [HttpPost("approval")]
        public async Task<ActionResult<LeaveAction>> LeaveAction([FromQuery] RequestSpecParams requestParams)
        {
            var spec = new RequestsByTeamSpecification(requestParams);
            var request = _service.GetRequestByIdNoTrack(spec).AsEnumerable().SingleOrDefault();
            var action = new LeaveAction
            {
                Action = ActionTaken.Approved,
                ActionBy = request.Employee.EmployeeCode,
                Leave = request,
                Reason = "Action taken on Request"
            };
            request.Status = ActionTaken.Approved;
            var result = await _service.UpdateLeave(request);
            if (result == null) return BadRequest(new ApiResponse(400, "Problem creating Leave"));
            var actionResult = await _actionService.CreateAction(action);
            NotifyProps notify = new NotifyProps
            {
                Type = "Approval",
                Message = "New Leave approval Approved",
                Team = request.Employee.Team,
                TeamRole = request.Employee.TeamRole,
                Employee = null

            };
            var notifyResult = await _notifyservice.AddNotification(notify);
            await _hubContext.Clients.All.BrodcastMessage(notify);
            return Ok();
        }
    }
}