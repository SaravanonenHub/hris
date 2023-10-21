using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Employees;

namespace Core.Specifications.EmployeeSpec
{
    public class EmployeeWithFilterSpec : BaseSpecification<Employee>
    {
        public EmployeeWithFilterSpec() : base()
        {
            AddInclude(x => x.Department);
            AddInclude(x => x.Designation);
            AddInclude(x => x.Branch);
            AddInclude(x => x.Division);
            // AddInclude(x => x.Team);
            // AddInclude(x => x.TeamRole);
            AddEnumValue(x => x.Status);

        }

        public EmployeeWithFilterSpec(int id, EmployeeFindSpec param) : base(x =>
            (string.IsNullOrEmpty(param.EmpCode) || x.EmployeeCode == param.EmpCode)
            && (!param.Id.HasValue || x.Id == param.Id)
            && (!param.IdNotEqual.HasValue || x.Id != param.IdNotEqual))
        {
            AddInclude(x => x.Department);
            AddInclude(x => x.Designation);
            AddInclude(x => x.Branch);
            AddInclude(x => x.Division);
            // AddInclude(x => x.Team);
            // AddInclude(x => x.TeamRole);
            // AddInclude(x => x.Department);
        }

        public EmployeeWithFilterSpec(EmployeeSpecParams param) : base(x =>
            (string.IsNullOrEmpty(param.Status) || x.Status == param.Status)
            && (string.IsNullOrEmpty(param.EmployeeNature) || x.EmployeeNature == param.EmployeeNature)
         && (string.IsNullOrEmpty(param.Role) || x.TeamRole == param.Role)
        && (string.IsNullOrEmpty(param.DepartmentId) || param.DepartmentId.Contains(x.Division.Id.ToString())))
        {
            
            AddInclude(x => x.Department);
            AddInclude(x => x.Designation);
            AddInclude(x => x.Branch);
            AddInclude(x => x.Division);
            // AddInclude(x => x.Team);
            // AddInclude(x => x.TeamRole);
        }
        
    }
    public class EmployeePersonalWithFilterSpec : BaseSpecification<EmployeePersonalInfo>
    {
        public EmployeePersonalWithFilterSpec(EmployeeFindSpec param) : base(x =>
            (string.IsNullOrEmpty(param.EmpCode) || x.Employee.EmployeeCode == param.EmpCode)
            && (!param.Id.HasValue || x.Id == param.Id)
            && (!param.IdNotEqual.HasValue || x.Id != param.IdNotEqual))
        {
        }
    }
    public class EmployeeExpWithFilterSpec : BaseSpecification<EmployeeExperienceInfo>
    {
        public EmployeeExpWithFilterSpec(EmployeeFindSpec param) : base(x =>
            (string.IsNullOrEmpty(param.EmpCode) || x.Employee.EmployeeCode == param.EmpCode)
            && (!param.Id.HasValue || x.Id == param.Id)
            && (!param.IdNotEqual.HasValue || x.Id != param.IdNotEqual))
        {
        }
    }
}