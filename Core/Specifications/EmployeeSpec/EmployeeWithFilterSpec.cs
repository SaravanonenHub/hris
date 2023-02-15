using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;

namespace Core.Specifications.EmployeeSpec
{
    public class EmployeeWithFilterSpec : BaseSpecification<Employee>
    {
        public EmployeeWithFilterSpec(EmployeeFindSpec param) : base(x =>
            (string.IsNullOrEmpty(param.EmpCode) || x.EmployeeCode == param.EmpCode)
            && (!param.Id.HasValue || x.Id == param.Id)
            && (!param.IdNotEqual.HasValue || x.Id != param.IdNotEqual))
        {
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