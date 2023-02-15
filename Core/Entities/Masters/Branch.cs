using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Masters
{
    [Table("T_BRANCH")]
    public class Branch : BaseInformation
    {
        [Required]
        [MaxLength(15)]
        public string BranchName { get; set; }
    }
}