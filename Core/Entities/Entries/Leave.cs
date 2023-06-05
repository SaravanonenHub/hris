using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Core.Entities.Actions;
using Core.Entities.Employees;
using Core.Entities.Notify;

namespace Core.Entities.Entries
{
    public enum Session
    {
        [EnumMember(Value = "Full Day")]
        FULLDAY,
        [EnumMember(Value = "1st Half Day")]
        FIRSTHALF,
        [EnumMember(Value = "2nd Half Day")]
        SECONDHALF,
    }

    public enum CancellationStatus
    {
        [EnumMember(Value = "No")]
        No,
        [EnumMember(Value = "Yes")]
        Yes,


    }
    public class Leave : BaseInformation
    {
        public Employee Employee { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string LeaveType { get; set; }
        public string Session { get; set; }
        public int Days { get; set; }
        public string Reason { get; set; }
        public ActionTaken Status { get; set; }
        public List<LeaveAction> Actions { get; set; }
        // public NotifyProps Notify { get; set; }


    }
    [Table("T_LEAVE_TYPE")]
    public class LeaveType : BaseInformation
    {
        [Required]
        public string EntitleName { get; set; }

    }
}