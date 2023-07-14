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
    }
    public class LeavePolicyDetailSpec : BaseSpecification<LeavePolicyDetails>
    {
        public LeavePolicyDetailSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.LeaveType);
            // AddInclude(x => x.Role);
        }
        public LeavePolicyDetailSpec() : base()
        {
            AddInclude(x => x.LeaveType);
        }
    }
}
