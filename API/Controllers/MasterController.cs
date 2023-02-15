using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.MasterDtos;
using API.Errors;
using Core.Entities.Masters;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Core.Interfaces;
using Core.Specifications.MasterSpec;

namespace API.Controllers
{

    public class MasterController : BaseApiController
    {
        private readonly IMasterRepository _service;
        private readonly IMapper _mapper;
        public MasterController(IMasterRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        #region BranchEndpoints
        [HttpGet("branches")]
        public async Task<IReadOnlyList<Branch>> GetBranches()
        {
            var results = await _service.GetBranchesAsync();
            return results;
        }
        [HttpGet("branch/{id}")]
        public async Task<ActionResult<Branch>> GetBranchById(int Id)
        {
            var result = await _service.GetBranchById(Id);
            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));

            return Ok(result);
        }
        [HttpPost("branch")]
        public async Task<ActionResult<Branch>> CreateBranch(BranchDto branchDto)
        {
            var alreadyExist = await _service.GetBranchByName(branchDto.BranchName);
            if (alreadyExist != null)
            {
                return BadRequest(new ApiResponse(400, "Branch name already exist!"));
            }
            var _branch = _mapper.Map<BranchDto, Branch>(branchDto);

            var result = await _service.CreateBranch(_branch);
            if (result == null) return BadRequest(new ApiResponse(400, "Problem creating branch"));

            return Ok(result);
        }

        [HttpPut("branch/{Id}")]
        public async Task<ActionResult<Branch>> UpdateBranch(int Id, BranchDto branchDto)
        {
            var IsExist = _service.GetBranchbyNoTrack(Id).AsEnumerable().SingleOrDefault();
            if (IsExist != null)
            {
                var alreadyExist = await _service.CheckBranchonUpdate(branchDto.BranchName, Id);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Branch name already exist!"));
                }
                var branch = _mapper.Map<BranchDto, Branch>(branchDto);
                branch.CreateDate = IsExist.CreateDate;
                branch.LastModifiedDate = DateTime.Now;
                branch.LastModifiedBy = "User";
                branch.Id = Id;
                var result = await _service.UpdateBranch(branch);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem updating branch"));
                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Id doesn't exist!"));
            }
        }

        #endregion
        #region DivisionEndPoints
        [HttpGet("divisions")]
        public async Task<IReadOnlyList<Division>> GetDivisions()
        {
            var results = await _service.GetDivisionesAsync();
            return results;
        }
        [HttpGet("division/{id}")]
        public async Task<ActionResult<Division>> GetDivisionById(int Id)
        {
            var result = await _service.GetDivisionById(Id);
            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));

            return Ok(result);
        }
        [HttpPost("division")]
        public async Task<ActionResult<Division>> CreateDivision(DivisionDto divisionDto)
        {
            var alreadyExist = await _service.GetDivisionByName(divisionDto.DivisionName);
            if (alreadyExist != null)
            {
                return BadRequest(new ApiResponse(400, "Division name already exist!"));
            }
            var _division = _mapper.Map<DivisionDto, Division>(divisionDto);

            var result = await _service.CreateDivision(_division);
            if (result == null) return BadRequest(new ApiResponse(400, "Problem creating division"));

            return Ok(result);
        }

        [HttpPut("division/{Id}")]
        public async Task<ActionResult<Division>> UpdateDivision(int Id, DivisionDto divisionDto)
        {
            var IsExist = _service.GetDivisionbyNoTrack(Id).AsEnumerable().SingleOrDefault();
            if (IsExist != null)
            {
                var alreadyExist = await _service.CheckDivisiononUpdate(divisionDto.DivisionName, Id);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Division name already exist!"));
                }
                var division = _mapper.Map<DivisionDto, Division>(divisionDto);
                division.CreateDate = IsExist.CreateDate;
                division.LastModifiedDate = DateTime.Now;
                division.LastModifiedBy = "User";
                division.Id = Id;
                var result = await _service.UpdateDivision(division);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem updating division"));
                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Id doesn't exist!"));
            }
        }
        #endregion
        #region  DepartmentEndPoints
        [HttpGet("departments")]
        public async Task<IReadOnlyList<Department>> GetDepartments()
        {

            var results = await _service.GetDepartmentesAsync();
            return results;
        }
        [HttpGet("department/{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int Id)
        {
            var result = await _service.GetDepartmentById(Id);
            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));

            return Ok(result);
        }
        [HttpPost("department")]
        public async Task<ActionResult<Department>> CreateDepartment(DepartmentDto departmentDto)
        {
            var alreadyExist = await _service.GetDepartmentByName(departmentDto.DepartmentName);
            if (alreadyExist != null)
            {
                return BadRequest(new ApiResponse(400, "Department name already exist!"));
            }
            var _dept = _mapper.Map<DepartmentDto, Department>(departmentDto);
            var division = await _service.GetDivisionById(departmentDto.DivisionId);
            _dept.Division = division;
            var result = await _service.CreateDepartment(_dept);
            if (result == null) return BadRequest(new ApiResponse(400, "Problem creating Department"));

            return Ok(result);
        }

        [HttpPut("department/{Id}")]
        public async Task<ActionResult<Department>> UpdateDepartment(int Id, DepartmentDto departmentDto)
        {
            var IsExist = _service.GetDepartmentbyNoTrack(Id).AsEnumerable().SingleOrDefault();
            if (IsExist != null)
            {
                var alreadyExist = await _service.CheckDepartmentonUpdate(departmentDto.DepartmentName, Id);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Department name already exist!"));
                }
                var dept = _mapper.Map<DepartmentDto, Department>(departmentDto);
                var division = await _service.GetDivisionById(departmentDto.DivisionId);
                dept.Division = division;
                dept.CreateDate = IsExist.CreateDate;
                dept.LastModifiedDate = DateTime.Now;
                dept.LastModifiedBy = "User";
                dept.Id = Id;
                var result = await _service.UpdateDepartment(dept);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem updating department"));
                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Id doesn't exist!"));
            }
        }
        #endregion
        #region DesignationEndPoints
        [HttpGet("designations")]
        public async Task<IReadOnlyList<Designation>> GetDesignations()
        {
            var results = await _service.GetDesignationesAsync();
            return results;
        }
        [HttpGet("designation/{id}")]
        public async Task<ActionResult<Designation>> GetDesignationById(int Id)
        {
            var result = await _service.GetDesignationById(Id);
            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));

            return Ok(result);
        }
        [HttpPost("designation")]
        public async Task<ActionResult<Designation>> CreateDesignation(DesignationDto designationDto)
        {
            var alreadyExist = await _service.GetDepartmentByName(designationDto.DesignationName);
            if (alreadyExist != null)
            {
                return BadRequest(new ApiResponse(400, "Designation name already exist!"));
            }
            var _designation = _mapper.Map<DesignationDto, Designation>(designationDto);

            var result = await _service.CreateDesignation(_designation);
            if (result == null) return BadRequest(new ApiResponse(400, "Problem creating Designation"));

            return Ok(result);
        }

        [HttpPut("designation/{Id}")]
        public async Task<ActionResult<Designation>> UpdateDesignation(int Id, DesignationDto designationDto)
        {
            var IsExist = _service.GetDepartmentbyNoTrack(Id).AsEnumerable().SingleOrDefault();
            if (IsExist != null)
            {
                var alreadyExist = await _service.CheckDesignationonUpdate(designationDto.DesignationName, Id);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Designation name already exist!"));
                }
                var designation = _mapper.Map<DesignationDto, Designation>(designationDto);
                designation.CreateDate = IsExist.CreateDate;
                designation.LastModifiedDate = DateTime.Now;
                designation.LastModifiedBy = "User";
                designation.Id = Id;
                var result = await _service.UpdateDesignation(designation);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem updating designation"));
                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Id doesn't exist!"));
            }
        }
        #endregion
    }
}