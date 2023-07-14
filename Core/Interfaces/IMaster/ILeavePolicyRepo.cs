using Core.Entities.Masters;

namespace Core.Interfaces.IMaster
{
    public interface ILeavePolicyRepo
    {
        Task<IReadOnlyList<LeavePolicy>> GetLeavePoliciesAsync();
        Task<LeavePolicy> GetbyName(string name);
        Task<LeaveType> GetLeaveTypeById(int id);
        Task<LeavePolicy> GetLeavePolicyById(int id);
        Task<LeavePolicy> Create(LeavePolicy entity);
        Task<LeavePolicy> Update(LeavePolicy entity);
        IQueryable<LeavePolicy> GetLeavePolicybyNoTrack(int id);
        Task<LeavePolicy> CheckPolicyonUpdate(string name, int id);
    }
}
