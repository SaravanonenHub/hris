using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.EmployeeDtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Employees;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class EmployeeExperianceController : BaseApiController
    {
        private readonly IEmployeeRepository _service;
        private readonly IMapper _mapper;

        public EmployeeExperianceController(IEmployeeRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("experiences")]
        public async Task<IReadOnlyList<EmployeeExperienceInfo>> GetEmployees()
        {
            var results = await _service.GetEmployeeExperiencesAsync();
            return results;
        }
        [HttpGet("experience/{id}")]
        public async Task<ActionResult<EmployeeExperienceInfo>> GetEmployeeById(int Id)
        {
            var result = await _service.GetEmployeeExperienceById(Id);
            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));

            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<ActionResult<EmpExperianceResponseDto>> CreateEmployee(EmpExperianceRequestDto empDto)
        {
            if (ModelState.IsValid)
            {
                var alreadyExist = await _service.GetEmployeeExperienceById(empDto.EmployeeID, null);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Employee code already exist!"));
                }
                var _empExp = _mapper.Map<EmpExperianceRequestDto, EmployeeExperienceInfo>(empDto);
                var _emp = await _service.GetEmployeeById(empDto.EmployeeID);
                _empExp.Employee = _emp;
                var result = await _service.CreateEmployeeExperience(_empExp);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem creating employee"));

                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Model Invalid!"));
            }

        }
        [HttpPut("update")]
        public async Task<ActionResult<EmpExperianceResponseDto>> UpdateEmployee(int Id, EmpExperianceRequestDto empDto)
        {
            var IsExist = _service.GetEmployeeExperienceByIdNoTrack(Id).AsEnumerable().SingleOrDefault();

            if (IsExist != null)
            {

                var _empExp = _mapper.Map<EmpExperianceRequestDto, EmployeeExperienceInfo>(empDto);
                var _emp = await _service.GetEmployeeById(empDto.EmployeeID);
                _empExp.Employee = _emp;
                _emp.CreateDate = IsExist.CreateDate;
                _emp.LastModifiedDate = DateTime.Now;
                _emp.LastModifiedBy = "User";
                _emp.Id = Id;
                var result = await _service.UpdateEmployeeExperience(_empExp);
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