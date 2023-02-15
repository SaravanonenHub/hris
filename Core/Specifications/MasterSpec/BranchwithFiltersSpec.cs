using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Masters;

namespace Core.Specifications.MasterSpec
{
    public class BranchwithFiltersSpec : BaseSpecification<Branch>
    {
        public BranchwithFiltersSpec(MasterUpdateSpecParam param) : base(x =>
            (string.IsNullOrEmpty(param.Search) || x.BranchName.ToLower().Contains(param.Search))
            && (!param.Id.HasValue || x.Id != param.Id))
        {
        }
    }
}