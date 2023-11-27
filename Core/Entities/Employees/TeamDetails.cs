using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Notify;

namespace Core.Entities.Employees
{
    [Table("T_APP_ROLE")]
    public class TeamRole : BaseInformation
    {
        [Required]
        [MaxLength(15)]
        public string Role { get; set; }
        [Required]
        public int HLevel { get; set; }
        public string HasApprovalAuth { get; set; }
        public IReadOnlyList<NotifyProps> Notifications { get; set; }

    }
    [Table("T_TEAM_DETAILS")]
    public class TeamDetails : BaseInformation
    {
        public TeamDetails()
        {
        }

        public TeamDetails(int Id, Employee employee, TeamRole role)
        {
            this.Id = Id;
            Employee = employee;
            Role = role;
            // CreateDate = Id > 0 ? createDate : DateTime.Now;
            // IsActive = "Y";
            // LastModifiedDate = DateTime.Now;
        }

        // [Required]
        // [MaxLength(30)]
        // public Team Team { get; set; }
        public int EmployeeId { get; set; }
        [Required]
        public Employee Employee { get; set; }
        public int RoleId { get; set; }
        [Required]
        public TeamRole Role { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }


    }
}