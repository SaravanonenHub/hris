using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        [Required]
        public string RoleType { get; set; }

        public AppRole(string name) : base(name)
        {
            // RoleType = type;
        }

        public AppRole()
        {
        }
    }
}