using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Identity
{
    public class AppUserDto
    {

        [Required(ErrorMessage = "Employee ID is required")]
        public string EmployeeCode { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}