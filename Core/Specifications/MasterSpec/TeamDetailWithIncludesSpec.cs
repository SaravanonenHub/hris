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
            AddInclude(x => x.Role);
        }
        public TeamDetailWithIncludesSpec() : base()
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Role);
        }
    }
}