﻿using Core.Entities.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Masters
{
    [Table("T_ROLE_MAPPING")]
    public class UserRoleMapping:BaseInformation
    {
        [Required]
        public TeamRole Role { get; set; }

        [Required]
        public TeamRole ReportingRole { get; set; }
    }
}
