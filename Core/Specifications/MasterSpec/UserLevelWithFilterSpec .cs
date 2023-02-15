using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Masters;

namespace Core.Specifications.MasterSpec
{
    public class UserLevelWithFilterSpec : BaseSpecification<UserLevel>
    {
        public UserLevelWithFilterSpec(MasterUpdateSpecParam param) : base(x =>
            (string.IsNullOrEmpty(param.Search) || x.UserLevelName.ToLower().Contains(param.Search))
            && (!param.Id.HasValue || x.Id != param.Id))
        {
        }
    }
}