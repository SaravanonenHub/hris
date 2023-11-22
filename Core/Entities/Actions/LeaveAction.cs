using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Entries;

namespace Core.Entities.Actions
{
    public static class ActionTaken
    {
        public static string Created { get; } = "Created";
        public static string Inapproval { get; } = "In Approval";
        public static string Inprogress { get; } = "In Progress";
        public static string Closed { get; } = "Closed";
    }
    public static class ActionSummary
    {
        public static string Created { get; set; } = "-Your {0} has been received on {1}";
        public static string Inapproval { get; } = "-system has route your request to approval state - {0}";
        public static string Inprogress { get; } = "-request has been route in progress state - {0}";
        public static string Closed { get; } = "-request has been closed by {0} with state {1} - {2}";
    }
    public static class RequestAction
    {
        public static string Submitted { get; } = "Submitted";
        public static string Approved { get; } = "Approved";
        public static string Rejected { get; } = "Rejected";
        public static string Cancelled { get; } = "Cancelled";



    }
    public class ActionHistory : ActionInformation
    {
        public Request Request { get; set; }
        public string Action { get; set; }
        public string Comment { get; set; }
        //public string Summary { get; set; }

    }
}