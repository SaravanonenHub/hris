using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Masters
{
    [Table("T_DIVISION")]
    public class Division : BaseInformation
    {
        [Required]
        [MaxLength(15)]
        public string DivisionName { get; set; }
        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }
    }
}