using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Masters
{
    public enum PolicyType
    {
        Yearly,
        Monthly
    }
    public class LeavePolicy : BaseInformation
    {
        public string PolicyName { get; set; }
        public string ShortName { get; set; }
        public PolicyType Type { get; set; }
        public IReadOnlyList<LeavePolicyDetails> LeavePolicyDetails { get; set; }

    }
    [Table("T_LEAVE_TYPE")]
    public class LeaveType : BaseInformation
    {
        [Required]
        [MaxLength(20)]
        public string LeaveName { get; set; }
        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }
        public IReadOnlyList<LeavePolicyDetails> LeavePolicyDetails { get; set; }
    }
    public class LeavePolicyDetails:BaseEntity
    {
     

        public LeavePolicy LeavePolicy { get; set; }
        public LeaveType LeaveType { get; set; }
        public int Day { get; set; }
    }
}
