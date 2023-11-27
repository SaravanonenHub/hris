using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Masters;
using Core.Entities.Notify;

namespace Core.Entities.Employees
{
    [Table("T_TEAM")]
    public class Team : BaseInformation
    {
        [Required]
        [MaxLength(15)]
        public string TeamName { get; set; }
        public string DisplayName { get; set; }
       
        public int DepartmentId { get; set; }
        [Required]
        public Department Department { get; set; }
        [Required]
        public IReadOnlyList<TeamDetails> TeamDetails { get; set; }
        public IReadOnlyList<NotifyProps> Notifications { get; set; }

    }
}