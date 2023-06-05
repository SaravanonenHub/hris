using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.EmployeeDtos;
using API.Dtos.MasterDtos;

namespace API.Dtos.ActionDtos
{
    public class NotificationDtos
    {
        [Required]
        public string Type { get; set; }

        public TeamDto Team { get; set; } = null;
        public UserRoleDto Role { get; set; } = null;
        public EmployeeCommonDto Employee { get; set; } = null;
    }
}