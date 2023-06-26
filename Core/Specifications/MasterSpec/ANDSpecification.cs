using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;

namespace Core.Specifications.MasterSpec
{
    public class ANDSpecification : BaseSpecification<Team>
    {
        private readonly ISpecification<Team> _spec;
        private readonly ISpecification<TeamDetails> _spec2;

        public ANDSpecification(ISpecification<Team> spec, ISpecification<TeamDetails> spec2)
        {
            _spec = spec;
            _spec2 = spec2;
        }
    }
}