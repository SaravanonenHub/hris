using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Employees
{
    [Table("T_TEAM_ROLE")]
    public class TeamRole : BaseInformation
    {
        [Required]
        [MaxLength(15)]
        public string Role { get; set; }
        [Required]
        public int HLevel { get; set; }
        public string HasApprovalAuth { get; set; }

    }
    [Table("T_TEAM_DETAILS")]
    public class TeamDetails : BaseEntity
    {
        public TeamDetails()
        {
        }

        public TeamDetails(Team team, Employee employee, TeamRole role)
        {
            // Team = team;
            Employee = employee;
            Role = role;
        }

        // [Required]
        // [MaxLength(30)]
        // public Team Team { get; set; }
        [Required]
        public Employee Employee { get; set; }
        [Required]
        public TeamRole Role { get; set; }
    }
}