using API.Dtos.MasterDtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Masters;
using Core.Interfaces.IMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class LeavePolicyController : BaseApiController
    {
        private readonly ILeavePolicyRepo _service;
        private readonly IMapper _mapper;

        public LeavePolicyController(ILeavePolicyRepo service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("LeavePolicies")]
        public async Task<IReadOnlyList<LeavePolicyResponseDto>> GetLeavePolicy()
        {
            var _policies = await _service.GetLeavePoliciesAsync();
            var results = _mapper.Map<IReadOnlyList<LeavePolicy>, IReadOnlyList<LeavePolicyResponseDto>>(_policies);
            return results;
        }

        [HttpGet("LeavePolicy/{id:int}")]
        public async Task<LeavePolicyResponseDto> GetLeavePolicyById(int id)
        {
            var _policy = await _service.GetLeavePolicyById(id);
            var results = _mapper.Map<LeavePolicy, LeavePolicyResponseDto>(_policy);
            return results;
        }

        [HttpPost("create")]
        public async Task<ActionResult<LeavePolicyResponseDto>> CreateLeaveType(LeavePolicyDto _reqDto)
        {
            if (ModelState.IsValid)
            {
                var alreadyExist = await _service.GetbyName(_reqDto.PolicyName);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Team name already exist!"));
                }
                var _policy = _mapper.Map<LeavePolicyDto, LeavePolicy>(_reqDto);
                var details = new List<LeavePolicyDetails>();
                foreach (var detail in _reqDto.LeavePolicyDetails)
                {
                    var _leaveType = await _service.GetLeaveTypeById(detail.LeaveTypeID);
                    var teamDetail = new LeavePolicyDetails() { LeaveType= _leaveType, Day = detail.Days};
                    details.Add(teamDetail);

                }


                _policy.LeavePolicyDetails = details;
                var result = await _service.Create(_policy);

                if (result == null) return BadRequest(new ApiResponse(400, "Problem creating policy"));
                var results = _mapper.Map<LeavePolicy, LeavePolicyResponseDto>(result);
                return Ok(results);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Model Invalid!"));
            }
        }

        [HttpPut("policy/{Id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LeavePolicyResponseDto>> UpdateTeam(int Id, LeavePolicyDto _reqDto)
        {
            var IsExist = _service.GetLeavePolicybyNoTrack(Id).AsEnumerable().SingleOrDefault();
            if (IsExist != null)
            {
                var alreadyExist = await _service.CheckPolicyonUpdate(_reqDto.PolicyName, Id);
                if (alreadyExist != null)
                {
                    return BadRequest(new ApiResponse(400, "Team name already exist!"));
                }
                var _policy = _mapper.Map<LeavePolicyDto,LeavePolicy>(_reqDto);

                var details = new List<LeavePolicyDetails>();
                foreach (var detail in _reqDto.LeavePolicyDetails)
                {
                    var _leaveType = await _service.GetLeaveTypeById(detail.LeaveTypeID);
                    var teamDetail = new LeavePolicyDetails() { LeaveType = _leaveType, Day = detail.Days };
                    details.Add(teamDetail);

                }
                _policy.LeavePolicyDetails = details;
                _policy.LastModifiedDate = DateTime.Now;
                _policy.LastModifiedBy = "User";
                _policy.Id = Id;
                var result = await _service.Update(_policy);
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
