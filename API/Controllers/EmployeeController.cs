using API.Dtos.EmployeeDtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Employees;
using Core.Interfaces;
using Core.Interfaces.IMaster;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeRepository _service;
        private readonly IMasterRepository _masterService;
        private readonly ITeamRepository _teamService;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository service, IMapper mapper = null, IMasterRepository masterService = null, ITeamRepository teamService = null)
        {
            _service = service;
            _mapper = mapper;
            _masterService = masterService;
            _teamService = teamService;
        }
        [HttpGet("employees")]
        public async Task<IReadOnlyList<Employee>> GetEmployees()
        {
            var results = await _service.GetEmployeesAsync();
            return results;
        }
        [HttpGet("employee/{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int Id)
        {
            var result = await _service.GetEmployeeById(Id);
            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));

            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<ActionResult<EmployeeResponseDto>> CreateEmployee(EmployeeRequestDto empDto)
        {
            if (ModelState.IsValid)
            {
                var alreadyExist = await _service.GetEmployeeById(0, empDto.EmployeeCode);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Employee code already exist!"));
                }
                var _emp = _mapper.Map<EmployeeRequestDto, Employee>(empDto);
                var _branch = await _masterService.GetBranchById(empDto.BranchID);
                var _division = await _masterService.GetDivisionById(empDto.DivisionID);
                var _department = await _masterService.GetDepartmentById(empDto.DepartmentID);
                var _designation = await _masterService.GetDesignationById(empDto.DesignationID);
                var _team = await _teamService.GetTeamById(empDto.TeamId);
                var _teamRole = await _teamService.GetUserRoleById(empDto.TeamRoleId);
                _emp.Branch = _branch;
                _emp.Division = _division;
                _emp.Department = _department;
                _emp.Designation = _designation;
                _emp.Team = _team;
                _emp.TeamRole = _teamRole;
                var result = await _service.CreateEmployee(_emp);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem creating employee"));

                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Model Invalid!"));
            }

        }
        [HttpPut("update")]
        public async Task<ActionResult<EmployeeResponseDto>> UpdateEmployee(int Id, EmployeeRequestDto empDto)
        {
            var IsExist = _service.GetEmployeeByIdNoTrack(Id).AsEnumerable().SingleOrDefault();

            if (IsExist != null)
            {

                var alreadyExist = await _service.CheckEmployeeonUpdate(empDto.EmployeeCode, Id);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Employee code already exist!"));
                }
                var _emp = _mapper.Map<EmployeeRequestDto, Employee>(empDto);
                var _branch = await _masterService.GetBranchById(empDto.BranchID);
                var _division = await _masterService.GetDivisionById(empDto.DivisionID);
                var _department = await _masterService.GetDepartmentById(empDto.DepartmentID);
                var _designation = await _masterService.GetDesignationById(empDto.DesignationID);
                // var _userLevel = await _masterService.GetUserLevelById(empDto.UserLevelId);
                _emp.Branch = _branch;
                _emp.Division = _division;
                _emp.Department = _department;
                _emp.Designation = _designation;
                // _emp.UserLevel = _userLevel;
                _emp.CreateDate = IsExist.CreateDate;
                _emp.LastModifiedDate = DateTime.Now;
                _emp.LastModifiedBy = "User";
                _emp.Id = Id;
                var result = await _service.UpdateEmployee(_emp);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem creating employee"));

                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Model Invalid!"));
            }

        }
    }
}