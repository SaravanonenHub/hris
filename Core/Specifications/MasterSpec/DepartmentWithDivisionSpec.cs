using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Masters;

namespace Core.Specifications.MasterSpec
{
    public class DepartmentWithDivisionSpec : BaseSpecification<Department>
    {
        public DepartmentWithDivisionSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Division);
        }
        public DepartmentWithDivisionSpec() : base()
        {
            AddInclude(x => x.Division);
        }
    }
}