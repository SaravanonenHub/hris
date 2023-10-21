using Core.Entities.Masters;
using Core.Specifications.MasterSpec;

namespace Core.Interfaces.IMaster
{
    public interface ILeavePolicyRepo
    {
        Task<IReadOnlyList<LeavePolicy>> GetLeavePoliciesAsync();
        Task<LeavePolicy> GetbyName(string name);
        Task<LeaveType> GetLeaveTypeById(int id);
        Task<LeavePolicy> GetLeavePolicyById(int id);
        Task<LeavePolicy> GetLeavePolicyByIdWithFilter(LeavePolicySpec spec);
        Task<LeavePolicy> Create(LeavePolicy entity);
        Task<LeavePolicy> Update(LeavePolicy entity);
        IQueryable<LeavePolicy> GetLeavePolicybyNoTrack(int id);
        Task<LeavePolicy> CheckPolicyonUpdate(string name, int id);
    }
}
