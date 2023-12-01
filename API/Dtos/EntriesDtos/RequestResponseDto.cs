using API.Dtos.ActionDtos;
using API.Dtos.EmployeeDtos;
using Core.Entities.Entries;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Dtos.EntriesDtos
{
    public class RequestResponseDto
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public RequestTemplateDto Type { get; set; }
        public EmployeeCommonDto Employee { get; set; }
        public string Description { get; set; }
        public string CurrentState { get; set; }
        public string Status { get; set; }
        public char CancellationStatus { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestDate { get; set; }
    }
    public class RequestDetailResponseDto
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public RequestTemplateDto Type { get; set; }
        public EmployeeCommonDto Employee { get; set; }
        public IReadOnlyList<ActionResponseDtos> Actions { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public char CancellationStatus { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestDate { get; set; }
    }
    public class RequestEntriesResponseDto 
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public RequestTemplateDto Type { get; set; }
        public string TypeName { get; set; }
        public EmployeeCommonDto Employee { get; set; }
        public IReadOnlyList<ActionResponseDtos> Actions { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public char CancellationStatus { get; set; }
        public string RequestedBy { get; set; }
        public DateTime RequestDate { get; set; }
        //public ICollection<T> Entries { get; set; }
        [JsonPropertyName("entries")]
        public LeaveResponseDto? Leave { get; set; }
    }
    public class RequestApprovalDto:RequestResponseDto
    {

    }
    public class RequestTemplateDto
    {
    
        public string TemplateName { get; set; }
   
        public string TemplatePrefix { get; set; }
        public string Description { get; set; }
 
        public string MainTableName { get; set; }
   
        public string ServiceName { get; set; }
   
        public string MethodName { get; set; }
    }
}
