using Core.Entities.Masters;

namespace API.Dtos.MasterDtos
{
    public class LeavePolicyDto
    {
        public string PolicyName { get; set; }
        public string ShortName { get; set; }
        public IReadOnlyList<LeavePolicyDetailDto> LeavePolicyDetails { get; set; }
    }
    public class LeavePolicyDetailDto
    {
        public int LeaveTypeID { get; set; }
        public int Days { get; set; }
    }
   
    public class LeavePolicyResponseDto
    {
        public int Id { get; set; }
        public string PolicyName { get; set; }
        public string ShortName { get; set; }
        public IReadOnlyList<LeavePolicyDetailResponseDto> LeavePolicyDetails { get; set; }
    }
    public class LeavePolicyDetailResponseDto
    {
        public LeaveTypeResponseDto LeaveType { get; set; }
        public int Day { get; set; }
    }
    public class LeaveEntitlement
    {
        public int Id { get; set; }
        public string PolicyName { get; set; }
        public string ShortName { get; set; }
        public List<LeaveEntitlementDetail> Details { get; set; } = new List<LeaveEntitlementDetail>();

    }
    public class LeaveEntitlementDetail
    {
        public LeaveTypeResponseDto LeaveType { get; set; }
        public int Provided { get; set; }
        public int Taken { get; set; }
    }
    public class LeaveTypeResponseDto 
    {
        public int Id { get; set; }
        public string LeaveName { get; set; }
        public string ShortName { get; set; }
    }
}
