using Core.Entities.Masters;

namespace Core.Specifications.MasterSpec
{
    public class LeavePolicySpec : BaseSpecification<LeavePolicy>
    {
        public LeavePolicySpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.LeavePolicyDetails);
            AddInclude($"{nameof(LeavePolicy.LeavePolicyDetails)}.{nameof(LeavePolicyDetails.LeaveType)}");
        }
        public LeavePolicySpec():base()
        {
            AddInclude(x => x.LeavePolicyDetails);
            AddInclude($"{nameof(LeavePolicy.LeavePolicyDetails)}.{nameof(LeavePolicyDetails.LeaveType)}");
        }
        public LeavePolicySpec(int id, LeavePolicyFilterParams param) : base(x =>
             (string.IsNullOrEmpty(param.PolicyName) || x.PolicyName == param.PolicyName)
             && (string.IsNullOrEmpty(param.LeaveType) || x.LeavePolicyDetails.Any(y => y.LeaveType != null && y.LeaveType.LeaveName == param.LeaveType))
            && (x.Id == id))
        {

            AddInclude(x => x.LeavePolicyDetails);
            AddInclude($"{nameof(LeavePolicy.LeavePolicyDetails)}.{nameof(LeavePolicyDetails.LeaveType)}");
            // AddInclude(x => x.Team);
            // AddInclude(x => x.TeamRole);
        }
    }
    public class LeavePolicyDetailSpec : BaseSpecification<LeavePolicyDetails>
    {
        public LeavePolicyDetailSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.LeaveType);
            // AddInclude(x => x.Role);
        }
        public LeavePolicyDetailSpec(string leaveType) : 
                base(x => string.IsNullOrEmpty(leaveType) || x.LeaveType.LeaveName == leaveType)
        {
            AddInclude(x => x.LeaveType);
        }
        public LeavePolicyDetailSpec() : base()
        {
            AddInclude(x => x.LeaveType);
        }
    }
    
    public class LeavePolicyFilterParams
    {
        public int? EmpId { get; set; }

        public string? PolicyName { get; set; } = null;
        public string? LeaveType { get; set; } = null;
    }
}
