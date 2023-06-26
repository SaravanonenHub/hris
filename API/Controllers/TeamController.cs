using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.MasterDtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Employees;
using Core.Interfaces;
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
        public async Task<IReadOnlyList<Team>> GetTeams()
        {
            var results = await _service.GetTeamesAsync();
            return results;
        }
        [HttpGet("team/{id:int}")]
        public async Task<ActionResult<TeamDto>> GetTeamById(int id)
        {
            var result = await _service.GetTeamById(id);
            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));
            var _team = _mapper.Map<Team, TeamResponseDto>(result);

            return Ok(_team);
            // return Ok(result);
        }
        [HttpPost("team")]
        public async Task<ActionResult<Team>> CreateShift(TeamDto teamDto)
        {
            if (ModelState.IsValid)
            {
                var alreadyExist = await _service.GetTeamByName(teamDto.TeamName);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Team name already exist!"));
                }
                var _team = _mapper.Map<TeamDto, Team>(teamDto);
                var _department = await _service.GetDepartmentById(teamDto.DepartmentId);
                _team.Department = _department;
                var details = new List<TeamDetails>();
                foreach (var detail in teamDto.TeamDetails)
                {
                    var _role = await _service.GetUserRoleById(detail.RoleId);
                    var _emp = await _empService.GetEmployeeById(detail.EmployeeId);
                    var teamDetail = new TeamDetails(0, _emp, _role);
                    details.Add(teamDetail);

                }
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
        public async Task<ActionResult<Team>> UpdateTeam(int Id, TeamResponseDto teamDto)
        {
            var IsExist = _service.GetTeambyNoTrack(Id).AsEnumerable().SingleOrDefault();
            if (IsExist != null)
            {
                var alreadyExist = await _service.CheckTeamonUpdate(teamDto.TeamName, Id);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Team name already exist!"));
                }
                var _team = _mapper.Map<TeamResponseDto, Team>(teamDto);
                var _department = await _service.GetDepartmentById(teamDto.DepartmentId);
                _team.Department = _department;
                var details = new List<TeamDetails>();
                foreach (var detail in teamDto.TeamDetails)
                {
                    var _role = await _service.GetUserRoleById(detail.Role.Id);
                    var _emp = await _empService.GetEmployeeById(detail.Employee.Id);
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