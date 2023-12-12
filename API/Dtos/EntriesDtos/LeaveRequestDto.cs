using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.EmployeeDtos;
using API.Dtos.MasterDtos;
using Core.Entities.Masters;

namespace API.Dtos.EntriesDtos
{
    public class LeaveRequestDto
    {
        public string EmployeeId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LeaveType { get; set; }
        public string Session { get; set; }
        public float Days { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string CancellationStatus { get; set; }
        public string CreatedBy { get; set; }
        public int TemplateId { get; set; }
    }
    public class LeaveResponseDto
    {
        public int Id { get; set; }
        public RequestResponseDto Request { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string LeaveType { get; set; }
        public string Session { get; set; }
        public int Days { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string CancellationStatus { get; set; }
        public LeaveEntitlement Entitlement { get; set; }

    }
    
    public class LeaveTypeReqDto
    {
        public string EntitleName { get; set; }
    }
    public class LeaveTypeResDto
    {
        public string EntitleName { get; set; }
    }
    
}