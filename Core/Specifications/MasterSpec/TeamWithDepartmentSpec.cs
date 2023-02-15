using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Masters;

namespace Core.Specifications.MasterSpec
{
    public class TeamWithDepartmentSpec : BaseSpecification<Team>
    {
        public TeamWithDepartmentSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Department);
        }
        public TeamWithDepartmentSpec() : base()
        {
            AddInclude(x => x.Department);
        }
    }
}