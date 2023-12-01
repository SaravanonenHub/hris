using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;

namespace Core.Specifications.MasterSpec
{
    public class TeamDetailWithIncludesSpec : BaseSpecification<TeamDetails>
    {
        public TeamDetailWithIncludesSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Employee);
            // AddInclude(x => x.Role);
        }
        public TeamDetailWithIncludesSpec() : base()
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Role);
        }
        public TeamDetailWithIncludesSpec(TeamDetailFilterSpec filter) : base( x =>
             (!filter.Id.HasValue || x.Id == filter.Id)
            && (!filter.EmpId.HasValue || x.Employee.Id == filter.EmpId)
            && (!string.IsNullOrEmpty(filter.EmpCode) || x.Employee.EmployeeCode == filter.EmpCode)
            && (!filter.RoleId.HasValue || x.Role.Id == filter.EmpId))
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Role);
            AddInclude(x => x.Team);
        }
    }

    public class TeamDetailFilterSpec
    {
       
        public int? Id { get; set; }
        public int? EmpId { get; set; }
        public int? RoleId { get; set; }
        public string? EmpCode { get; set; }
    }
}