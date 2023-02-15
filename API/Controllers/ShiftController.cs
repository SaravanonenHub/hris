using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.MasterDtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Masters;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class ShiftController : BaseApiController
    {
        private readonly IMasterRepository _service;
        private readonly IMapper _mapper;
        public ShiftController(IMasterRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("shifts")]
        public async Task<IReadOnlyList<Shift>> GetShifts()
        {
            var results = await _service.GetShiftesAsync();
            return results;
        }
        [HttpGet("shift/{id}")]
        public async Task<ActionResult<Shift>> GetShiftById(int Id)
        {
            var result = await _service.GetShiftById(Id);
            if (result == null) return BadRequest(new ApiResponse(400, "Id doesn't exist!"));

            return Ok(result);
        }
        [HttpPost("shift")]
        public async Task<ActionResult<Shift>> CreateShift(ShiftDto shiftDto)
        {
            if (ModelState.IsValid)
            {
                var alreadyExist = await _service.GetShiftByName(shiftDto.ShiftName);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Shift name already exist!"));
                }
                var _shift = _mapper.Map<ShiftDto, Shift>(shiftDto);

                var result = await _service.CreateShift(_shift);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem creating shift"));

                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Model Invalid!"));
            }

        }

        [HttpPut("shift/{Id}")]
        public async Task<ActionResult<Shift>> UpdateShift(int Id, ShiftDto shiftDto)
        {
            var IsExist = _service.GetShiftbyNoTrack(Id).AsEnumerable().SingleOrDefault();
            if (IsExist != null)
            {
                var alreadyExist = await _service.CheckShiftonUpdate(shiftDto.ShiftName, Id);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Shift name already exist!"));
                }
                var shift = _mapper.Map<ShiftDto, Shift>(shiftDto);
                shift.CreateDate = IsExist.CreateDate;
                shift.LastModifiedDate = DateTime.Now;
                shift.LastModifiedBy = "User";
                shift.Id = Id;
                var result = await _service.UpdateShift(shift);
                if (result == null) return BadRequest(new ApiResponse(400, "Problem updating shift"));
                return Ok(result);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Id doesn't exist!"));
            }
        }
    }
}