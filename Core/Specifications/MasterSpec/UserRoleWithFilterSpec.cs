using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Masters;

namespace Core.Specifications.MasterSpec
{
    public class UserRoleWithFilterSpec : BaseSpecification<TeamRole>
    {
        public UserRoleWithFilterSpec(MasterUpdateSpecParam param) : base(x =>
            (string.IsNullOrEmpty(param.Search) || x.Role.ToLower().Contains(param.Search))
            && (!param.Id.HasValue || x.Id != param.Id))
        {
        }
    }
    public class RoleMappingSpec : BaseSpecification<UserRoleMapping>
    {
        public RoleMappingSpec(int id) : base(x =>
            (x.Role.Id == id))
        {
            AddInclude(x => x.Role);
            AddInclude(x => x.ReportingRole);
        }
    }
}