using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Entries;
using Core.Specifications.EntriesSpec;

namespace Core.Interfaces.IEntries
{
    public interface ILeaveService
    {
        Task<Employee> GetEmployees();
        Task<bool> AlreadyExists(int empID, DateTime fDate, DateTime TDate);
        Task<Leave> SubmitLeave(Leave _leave);
        Task<Leave> UpdateLeave(Leave _leave);
        Task<IReadOnlyList<Leave>> GetReqForApproval(RequestsByTeamSpecification specification);
        Task<Leave> GetRequestById(RequestsByTeamSpecification specification);
        IQueryable<Leave> GetRequestByIdNoTrack(RequestsByTeamSpecification specification);
    }
}