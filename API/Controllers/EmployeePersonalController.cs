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

    public class EmployeePersonalController : BaseApiController
    {
        private readonly IEmployeeRepository _service;
        private readonly IMapper _mapper;

        public EmployeePersonalController(IEmployeeRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("personalInfos")]
        public async Task<IReadOnlyList<EmployeePersonalInfo>> GetEmployees()
        {
            var results = await _service.GetEmployeesPersonalAsync();
            return results;
        }
        [HttpGet("personalInfo/{id}")]
        public async Task<ActionResult<EmployeePersonalInfo>> GetEmployeeById(int Id)
        {
            var result = await _service.GetEmployeePersonalById(Id);
            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));

            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<ActionResult<EmpPersonalResponseDto>> CreateEmployee(EmpPersonalRequestDto empDto)
        {
            if (ModelState.IsValid)
            {
                var alreadyExist = await _service.GetEmployeePersonalById(empDto.EmployeeID, null);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Employee code already exist!"));
                }
                var _empPersonal = _mapper.Map<EmpPersonalRequestDto, EmployeePersonalInfo>(empDto);
                var _emp = await _service.GetEmployeeById(empDto.EmployeeID);
                _empPersonal.Employee = _emp;
                var result = await _service.CreateEmployeePersonal(_empPersonal);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem creating employee"));

                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Model Invalid!"));
            }

        }
        [HttpPut("update")]
        public async Task<ActionResult<EmpPersonalResponseDto>> UpdateEmployee(int Id, EmpPersonalRequestDto empDto)
        {
            var IsExist = _service.GetEmployeePersonalByIdNoTrack(Id).AsEnumerable().SingleOrDefault();

            if (IsExist != null)
            {

                var _empPersonal = _mapper.Map<EmpPersonalRequestDto, EmployeePersonalInfo>(empDto);
                var _emp = await _service.GetEmployeeById(empDto.EmployeeID);
                _empPersonal.Employee = _emp;
                _emp.CreateDate = IsExist.CreateDate;
                _emp.LastModifiedDate = DateTime.Now;
                _emp.LastModifiedBy = "User";
                _emp.Id = Id;
                var result = await _service.UpdateEmployeePersonal(_empPersonal);
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