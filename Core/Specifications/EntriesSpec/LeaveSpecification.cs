using Core.Entities.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications.EntriesSpec
{
    public class LeaveSpecification : BaseSpecification<Leave>
    {
        public LeaveSpecification(LeaveSpecParams param)
            :base(x =>
           // (!param.Id.HasValue || x.Id == param.Id) &&
            (!param.RequestId.HasValue || x.RequestId == param.RequestId)
            // && (!string.IsNullOrEmpty(param.Status) || x.Status == param.Status) &&
            //(!string.IsNullOrEmpty(param.CancellationStatus.ToString()) || x.CancellationStatus == param.CancellationStatus)
            )
        {
        }
    }

    public class LeaveSpecParams
    {
        public int? Id { get; set; }
        public int? RequestId { get; set; }
        public string? Status { get; set; }
        public char? CancellationStatus { get; set; }
        public int? EmpId { get; set; }
    }
}
