using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Masters
{
    [Table("T_USER_ROLE")]
    public class UserRole : BaseInformation
    {
        [Required]
        public string Role { get; set; }
    }
}