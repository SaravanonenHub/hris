using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Actions;
using Core.Entities.Entries;

namespace Core.Specifications.EntriesSpec
{
    public class RequestsByTeamSpecification : BaseSpecification<Leave>
    {
        public RequestsByTeamSpecification() : base(x => 
            (x.Request.CurrentState == ActionTaken.Inprogress))
        {
            AddInclude(x => x.Request.Employee);
            AddInclude(x => x.Request.Employee.Department);
        }
        public RequestsByTeamSpecification(LeaveSpecParams param) : base(x =>
            (!param.EmpId.HasValue || x.Request.Employee.Id == param.EmpId))
        {
            AddInclude(x => x.Request);
            AddInclude(x => x.Request.Employee);
            AddInclude(x => x.Request.Employee.Department);
        }
        public RequestsByTeamSpecification(RequestSpecParams param)
                : base(x =>
                    //(string.IsNullOrEmpty(param.Search) || x.Request.Employee.DisplayName.ToLower().Contains(param.Search)) &&
                    // (!param.TeamId.HasValue || x.Employee.Team.Id == param.TeamId) &&
                    (!param.EmpId.HasValue || x.Request.Employee.Id == param.EmpId) &&
                    (!string.IsNullOrEmpty(param.Status) || x.Status == param.Status) &&
                    (!param.RequestId.HasValue || x.Id == param.RequestId))
        {
            AddInclude(x => x.Request);
            AddInclude(x => x.Request.Employee);
            AddInclude(x => x.Request.Employee.Department);
            // AddInclude(x => x.Employee.Team);
            // AddInclude(x => x.Employee.TeamRole);
        }

        public RequestsByTeamSpecification(string[] strIDS)
            : base(x => strIDS.Contains(x.Id.ToString()))
        {
            AddInclude(x => x.Request.Employee);
            AddInclude(x => x.Request.Employee.Department);
        }
        public RequestsByTeamSpecification(int id)
            :base(x => x.Id == id)
        {
            AddInclude(x => x.Request);
            AddInclude($"{nameof(Leave.Request)}.{nameof(Request.Employee)}");
            AddInclude($"{nameof(Leave.Request)}.{nameof(Request.Type)}");

        }

        
    }

}