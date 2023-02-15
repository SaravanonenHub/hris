using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Masters
{
    [Table("T_DESIGNATION")]
    public class Designation : BaseInformation
    {
        [Required]
        [MaxLength(15)]
        public string DesignationName { get; set; }
    }
}