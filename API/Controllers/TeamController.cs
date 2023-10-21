using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.MasterDtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Employees;
using Core.Interfaces;
using Core.Specifications.MasterSpec;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class TeamController : BaseApiController
    {
        private readonly ITeamRepository _service;
        private readonly IEmployeeRepository _empService;
        private readonly IMapper _mapper;

        public TeamController(ITeamRepository service, IMapper mapper, IEmployeeRepository empService = null)
        {
            _service = service;
            _mapper = mapper;
            _empService = empService;
        }
        [HttpGet("teams")]
        public async Task<ActionResult<IReadOnlyList<TeamResponseDto>>> GetTeams()
        {
            try
            {
                var results = await _service.GetTeamesAsync();
                var teams = _mapper.Map<IReadOnlyList<Team>,IReadOnlyList<TeamResponseDto>>(results);
                return Ok(teams);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        [HttpGet("team/{id:int}")]
        public async Task<ActionResult<TeamResponseDto>> GetTeamById(int id)
        {
            var result = await _service.GetTeamById(id);
            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));
            var _team = _mapper.Map<Team, TeamResponseDto>(result);
            _team.Manager = _team.TeamDetails.Select(x => x.Employee).Where(x => x.TeamRole == "Manager").FirstOrDefault();
            _team.TeamLeader = _team.TeamDetails.Select(x => x.Employee).Where(x => x.TeamRole == "TeamLeader").FirstOrDefault();
            _team.Members = _team.TeamDetails.Select(x => x.Employee).Where(x => x.TeamRole == "Member").ToList();
            return Ok(_team);
            // return Ok(result);
        }
        [HttpGet("employeeTeam/{empId}")]
        public async Task<ActionResult<IReadOnlyList<TeamDetailsResponseDto>>> GetTeamsByEmployee(int empId)
        {

            var teamdetailFilter = new TeamDetailFilterSpec() { EmpId = empId};
            var teams = await _service.GetEmployeeTeams(teamdetailFilter);
            var _teamDetail = _mapper.Map<IReadOnlyList<TeamDetails>, IReadOnlyList<TeamDetailsResponseDto>>(teams);
            return Ok(_teamDetail);
        }
        [HttpPost("create")]
        public async Task<ActionResult<Team>> CreateShift([FromBody] TeamDto teamDto)
        {
            if (ModelState.IsValid)
            {
                var alreadyExist = await _service.GetTeamByName(teamDto.TeamName);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Team name already exist!"));
                }
                var _team = _mapper.Map<TeamDto, Team>(teamDto);
                var details = new List<TeamDetails>();
                foreach (var detail in teamDto.TeamDetails)
                {
                    var _role = await _service.GetUserRoleByName(detail.RoleName);
                    var _emp = await _empService.GetEmployeeById(detail.EmployeeId);
                    var teamDetail = new TeamDetails(0, _emp, _role);
                    details.Add(teamDetail);

                }

                var _department = await _service.GetDepartmentById(teamDto.DepartmentId);
                _team.Department = _department;
                _team.TeamDetails = details;
                var result = await _service.CreateTeam(_team);

                if (result == null) return BadRequest(new ApiResponse(400, "Problem creating team"));
                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Model Invalid!"));
            }

        }

        [HttpPut("team/{Id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Team>> UpdateTeam(int Id, TeamUpdateDto teamDto)
        {
            var IsExist = _service.GetTeambyNoTrack(Id).AsEnumerable().SingleOrDefault();
            if (IsExist != null)
            {
                var alreadyExist = await _service.CheckTeamonUpdate(teamDto.TeamName, Id);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Team name already exist!"));
                }
                var _team = _mapper.Map<TeamUpdateDto, Team>(teamDto);
                var _department = await _service.GetDepartmentById(teamDto.DepartmentId);
                _team.Department = _department;
                var details = new List<TeamDetails>();
                foreach (var detail in teamDto.TeamDetails)
                {
                    //var _role = await _service.GetUserRoleById(detail.Role.Id);
                    var _role = await _service.GetUserRoleByName(detail.RoleName);
                    var _emp = await _empService.GetEmployeeById(detail.EmployeeId);
                    var teamDetail = new TeamDetails(detail.Id, _emp, _role);
                    details.Add(teamDetail);

                }
                _team.TeamDetails = details;
                _team.LastModifiedDate = DateTime.Now;
                _team.LastModifiedBy = "User";
                _team.Id = Id;
                var result = await _service.UpdateTeam(_team);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem creating team"));

                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Id doesn't exist!"));
            }
        }
    }
}