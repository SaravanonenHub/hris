using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Masters
{
    [Table("T_DEPARTMENT")]
    public class Department : BaseInformation
    {
        [Required]
        public Division Division { get; set; }
        [Required]
        [MaxLength(15)]
        public string DepartmentName { get; set; }
        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }
    }
}