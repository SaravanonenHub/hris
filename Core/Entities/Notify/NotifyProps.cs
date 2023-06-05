using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;

namespace Core.Entities.Notify
{
    public class NotifyProps : BaseInformation
    {

        public string Type { get; set; }
        public string Message { get; set; }
        public Team Team { get; set; }
        public TeamRole TeamRole { get; set; }
        public Employee Employee { get; set; }

    }
}