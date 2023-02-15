using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Employees
{
    [Table("T_EMPLOYEE_PERSONAL")]
    public class EmployeePersonalInfo : BaseInformation
    {
        // [Required]
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        [MaxLength(30)]
        public string FatherName { get; set; }
        [MaxLength(15)]
        public string MobileNumber { get; set; }
        [MaxLength(30)]
        public string PersonalEmailID { get; set; }
        [MaxLength(30)]
        public string EmergencyContactPerson { get; set; }
        [MaxLength(10)]
        public string EmergenctContactMobile { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}