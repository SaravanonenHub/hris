using API.Dtos.EmployeeDtos;
using Core.Entities.Entries;

namespace API.Dtos.EntriesDtos
{
    public class RequestDto
    {
        public EmployeeCommonDto Employee { get; set; }
        public int TypeId { get; set; }
        public RequestTemplate RequestType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public char CancellationStatus { get; set; }

    }
    public class RequestPatchModel
    {
        public int RequestId { get; set; }
        public string Status { get; set; }
    }
}
