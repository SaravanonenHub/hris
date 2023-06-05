using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Entries;

namespace Core.Specifications.EntriesSpec
{
    public class RequestsByTeamSpecification : BaseSpecification<Leave>
    {
        public RequestsByTeamSpecification(RequestSpecParams param)
                : base(x =>
                    (string.IsNullOrEmpty(param.Search) || x.Employee.DisplayName.ToLower().Contains(param.Search)) &&
                    (!param.TeamId.HasValue || x.Employee.Team.Id == param.TeamId) &&
                    (!param.EmpId.HasValue || x.Employee.Id == param.EmpId) &&
                    (!param.RequestId.HasValue || x.Id == param.RequestId)
                )
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Employee.Department);
            AddInclude(x => x.Employee.Team);
            AddInclude(x => x.Employee.TeamRole);
        }
    }
}