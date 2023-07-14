using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Masters;

namespace Core.Specifications.MasterSpec
{
    public class TeamWithFilterSpec : BaseSpecification<Team>
    {
        public TeamWithFilterSpec(MasterUpdateSpecParam param) : base(x =>
            (string.IsNullOrEmpty(param.Search) || x.TeamName.ToLower().Contains(param.Search))
            && (!param.Id.HasValue || x.Id != param.Id))
        {
        }

    }
    public class PolicyWithFilterSpec : BaseSpecification<LeavePolicy>
    {
        public PolicyWithFilterSpec(MasterUpdateSpecParam param) : base(x =>
            (string.IsNullOrEmpty(param.Search) || x.PolicyName.ToLower().Contains(param.Search))
            || (string.IsNullOrEmpty(param.Search) || x.ShortName.ToLower().Contains(param.Search))
            && (!param.Id.HasValue || x.Id != param.Id))
        {
        }

    }
    public class LeaveWithFilterSpec : BaseSpecification<LeaveType>
    {
        public LeaveWithFilterSpec(MasterUpdateSpecParam param) : base(x =>
            (string.IsNullOrEmpty(param.Search) || x.LeaveName.ToLower().Contains(param.Search))
            || (string.IsNullOrEmpty(param.Search) || x.ShortName.ToLower().Contains(param.Search))
            && (!param.Id.HasValue || x.Id != param.Id))
        {
        }

    }
}