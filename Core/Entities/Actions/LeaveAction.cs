using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Entries;

namespace Core.Entities.Actions
{
    public enum ActionTaken
    {
        Ongoing,
        Pending,
        Approved,
        Rejected,
        Cancelled
    }
    public class LeaveAction : ActionInformation
    {
        public Leave Leave { get; set; }
        public ActionTaken Action { get; set; }
        public string Reason { get; set; }

    }
}