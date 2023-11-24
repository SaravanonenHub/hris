using API.Dtos.EntriesDtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Entries;
using Core.Interfaces;
using Core.Interfaces.IEntries;
using Core.Specifications;
using Core.Specifications.EntriesSpec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class RequestController : BaseApiController
    {
        private readonly IRequestService _service;
        private readonly ILeaveService _leaveService;
        private readonly IMapper _mapper;
        public RequestController(IRequestService service, IMapper mapper, ILeaveService leaveService)
        {
            _service = service;
            _mapper = mapper;
            _leaveService = leaveService;
        }
        [HttpGet("requests")]
        public async Task<ActionResult<IReadOnlyList<RequestResponseDto>>> GetAllRequests(
            [FromQuery] RequestSpecParams requestParams)
        {
            var spec = new RequestSpec(requestParams);
            var requests = await _service.GetRequests(spec);
            if (!requests.Any()) return BadRequest(new ApiResponse(400, "Request Not found"));
            var data = _mapper.Map<IReadOnlyList<RequestResponseDto>>(requests);
            return Ok(data);
            ;
        }
        [HttpGet("request/{id:int}")]
        public async Task<ActionResult<RequestDetailResponseDto>> GetRequestById(int id)
        {
   
            var request = await _service.GetRequest(id);
            if (request == null) return BadRequest(new ApiResponse(400, "Request not found for given ID"));
            var reqType = request.Type.MainTableName;
            var response = _mapper.Map<RequestEntriesResponseDto>(request);
            switch (reqType)
            {
                case "T_LEAVE":
                    Console.WriteLine("Leave");
                    var param = new LeaveSpecParams() { RequestId = request.Id };
                    var spec = new LeaveSpecification(param);
                    var leave = await _leaveService.GetLeavebyRequestId(spec);
                    response.TypeName = response.Type.TemplateName;
                    if(leave != null) response.Leave = _mapper.Map<LeaveResponseDto>(leave);
                    break;
                default:
                    Console.WriteLine("Default");
                    break;
            }
            return Ok(response);
            //var entity = _service.GetEntity(request.Type.MainTableName,id);
            
            //return Ok(_mapper.Map<RequestDetailResponseDto>(request));
        }
    }
}
