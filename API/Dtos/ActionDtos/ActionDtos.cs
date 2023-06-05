using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.ActionDtos
{
    public class ActionDtos
    {
        public int LeaveId { get; set; }
        public string Action { get; set; }
        public string Reason { get; set; }
        public string ActionBy { get; set; }

    }
}