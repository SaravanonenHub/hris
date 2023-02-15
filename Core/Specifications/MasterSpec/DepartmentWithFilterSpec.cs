using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Masters;

namespace Core.Specifications.MasterSpec
{
    public class DepartmentWithFilterSpec : BaseSpecification<Department>
    {
        public DepartmentWithFilterSpec(MasterUpdateSpecParam param) : base(x =>
            (string.IsNullOrEmpty(param.Search) || x.DepartmentName.ToLower().Contains(param.Search))
            && (!param.Id.HasValue || x.Id != param.Id))
        {
        }
    }
}