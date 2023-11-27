using API.Dtos.EntriesDtos;
using API.Dtos.MasterDtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Actions;
using Core.Entities.Employees;
using Core.Entities.Entries;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.EntriesSpec;
using Core.Specifications.MasterSpec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ApproveController : BaseApiController
    {
        private readonly ITeamRepository _teamService;
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;
        public ApproveController(ITeamRepository teamService, IRequestService requestService, IMapper mapper)
        {
            _teamService = teamService;
            _requestService = requestService;
            _mapper = mapper;
        }
        [HttpGet("submitters/{empId:int}")]
        public async Task<ActionResult<IReadOnlyList<Employee>>> GetSubmitters(int empId)
        {
            var teamdetailFilter = new TeamDetailFilterSpec() { EmpId = empId };
            var teams = await _teamService.GetEmployeeTeams(teamdetailFilter);
            var _teamDetails = new List<TeamResponseDto>();
            if (!teams.Any()) return BadRequest(new ApiResponse(400, "Team not found for emloyee"));

            foreach (var team in teams)
            {
                var detail = await _teamService.GetTeamById(team.Team.Id);
                _teamDetails.Add(_mapper.Map<TeamResponseDto>(detail));
            }

            var _teamDetail = _mapper.Map<IReadOnlyList<TeamDetails>, IReadOnlyList<TeamDetailsResponseDto>>(teams);

            //get role mapped to his role
            var roleId = teams.Where(x => x.Employee.Id == empId).FirstOrDefault();
            var _rolesMapped = await _teamService.GetRolesMapped(roleId.Role.Id);

            //loop over team and get employees with having specific role
            var employees = _teamDetails
                            .SelectMany(x => x.TeamDetails)
                            .Where(emp =>
                                emp.Role.Role == _rolesMapped?
                                .Select(x => x.ReportingRole.Role)
                                .FirstOrDefault());
            return Ok(employees);
        }
        [HttpGet("approvals/{empId:int}")]
        public async Task<ActionResult<IReadOnlyList<RequestResponseDto>>> GetSubmittersRequest(int empId)
        {
            //get employee teams
            var teamdetailFilter = new TeamDetailFilterSpec() { EmpId = empId };
            var teams = await _teamService.GetEmployeeTeams(teamdetailFilter);
            var _teamDetails = new List<TeamResponseDto>();
            if (!teams.Any()) return BadRequest(new ApiResponse(400, "Team not found for emloyee"));

            foreach (var team in teams)
            {
                var detail = await _teamService.GetTeamById(team.Team.Id);
                _teamDetails.Add(_mapper.Map<TeamResponseDto>(detail));
            }

            var _teamDetail = _mapper.Map<IReadOnlyList<TeamDetails>, IReadOnlyList<TeamDetailsResponseDto>>(teams);

            //get role mapped to his role
            var roleId = teams.Where(x => x.Employee.Id == empId).FirstOrDefault();
            var _rolesMapped = await _teamService.GetRolesMapped(roleId.Role.Id);

            //loop over team and get employees with having specific role
            var employees = _teamDetails
                            .SelectMany(x => x.TeamDetails)
                            .Where(emp =>
                                emp.Role.Role == _rolesMapped?
                                .Select(x => x.ReportingRole.Role)
                                .FirstOrDefault())
                            .Select(emp => emp.Employee.Id).ToArray();
            string empIds = string.Join(",", employees);

            //List<int> employees = new List<int>();
            //if(teams != null)
            //{
            //    foreach(var team in _teamDetails)
            //    {
            //        if(team.TeamDetails != null)
            //        {
            //            foreach (var emp in team.TeamDetails)
            //            {
            //                if (emp.Role.Role == _rolesMapped.Select(x => x.ReportingRole.Role).FirstOrDefault())
            //                    employees.Add(emp.Employee.Id);
            //            }
            //        }

            //    }
            //}

            //
            var spec = new RequestSpec(
                new RequestSpecParams() 
                { 
                    EmpIds=empIds,
                    Status=ActionTaken.Created,
                    
                }
            );
            var requests = await _requestService.GetRequests(spec);
            //var employeesWithPendingLeave = requests.Where(x => employees.Contains(x.Employee.Id));
            return Ok(_mapper.Map<RequestResponseDto>(requests));
        }

        [HttpPatch("approval/{requestId:int}")]
        public async Task<ActionResult> ApproveApproval(int requestId, [FromBody] JsonPatchDocument<RequestDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest(new ApiResponse(400, "Model Invalid"));
            }
            var request = await _requestService.GetRequest(requestId);
            var requestDto = _mapper.Map<RequestDto>(request);
            patchDoc.ApplyTo(requestDto);
            _mapper.Map(requestDto, request);
            if(!ModelState.IsValid)
            { 
                return BadRequest(new ApiResponse(400, "Request not found"));
            }
            var result = await _requestService.UpdateRequest(request);
            return Ok(result);
        }

        [HttpPatch("bulkApproval/{requestIds}")]
        public async Task<ActionResult> BulkApproveApproval(string requestIds)
        {
            string[] requestIDs = requestIds.Split(",");
            var spec = new RequestSpec(new RequestSpecParams() { RequestIds = requestIds});
            var requests = await _requestService.GetRequests(spec);
            if (requests != null)
            {
                foreach (var req in requests)
                {
                    req.CurrentState = ActionTaken.Closed;
                    var result = await _requestService.UpdateRequest(req);
                }
            }
            //var data = _mapper.Map<IReadOnlyList<LeaveResponseDto>>(request);
            return Ok();
        }
    }
}
