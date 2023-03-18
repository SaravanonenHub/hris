using System.Net.Http.Headers;
using API.Dtos.EmployeeDtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Employees;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Interfaces.IMaster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeRepository _service;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMasterRepository _masterService;
        private readonly ITeamRepository _teamService;
        private readonly IMapper _mapper;
        public static IWebHostEnvironment _environment;
        public EmployeeController(IEmployeeRepository service, IWebHostEnvironment environment, UserManager<AppUser> userManager
        , IMapper mapper = null, IMasterRepository masterService = null, ITeamRepository teamService = null)
        {
            _service = service;
            _mapper = mapper;
            _masterService = masterService;
            _teamService = teamService;
            _environment = environment;
            _userManager = userManager;
            // _userManager = userManager;
        }
        [HttpGet("employees")]
        public async Task<IReadOnlyList<Employee>> GetEmployees()
        {
            var results = await _service.GetEmployeesAsync();
            return results;
        }
        [HttpGet("employee/{id}")]
        public async Task<ActionResult<EmployeeResponseDto>> GetEmployeeById(int Id)
        {
            var result = await _service.GetEmployeeById(Id);

            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));
            var _emp = _mapper.Map<Employee, EmployeeResponseDto>(result);
            return Ok(_emp);
        }
        [HttpPost("create")]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<EmployeeResponseDto>> CreateEmployee([FromForm] EmployeeRequestDto empDto)
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



                var file = empDto.EmpImage;
                var folderName = Path.Combine("Resources", "Images");
                // string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                // if (!Directory.Exists(uploadsFolder))
                // {
                //     Directory.CreateDirectory(uploadsFolder);
                // }
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var modifiedFileName = string.Concat(_emp.EmployeeCode, System.IO.Path.GetExtension(file.FileName));
                    var fullPath = Path.Combine(pathToSave, modifiedFileName);
                    var dbPath = Path.Combine(folderName, modifiedFileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    _emp.ImagePath = dbPath;
                }
                else
                {
                    return BadRequest();
                }
                var result = await _service.CreateEmployee(_emp);
                if (result == null)
                {
                    return BadRequest(new ApiResponse(400, "Problem creating employee"));
                }
                else
                {
                    // var user = _mapper.Map<AppUser>(empDto);
                    var user = new AppUser
                    {
                        UserName = empDto.EmployeeCode,
                        DisplayName = empDto.DisplayName,
                        Email = empDto.EmailID
                    };
                    var pwd = "Mil@cr0n_" + empDto.EmployeeCode;
                    var userResult = await _userManager.CreateAsync(user, pwd);

                    // await _service.CreateUser(appUser);
                }

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