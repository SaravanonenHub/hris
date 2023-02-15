using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.EmployeeDtos
{
    public class EmpPersonalRequestDto
    {
        [Required]
        public int EmployeeID { get; set; }

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
    public class EmpPersonalResponseDto
    {

        public EmployeeCommonDto Employee { get; set; }

        public string FatherName { get; set; }

        public string MobileNumber { get; set; }

        public string PersonalEmailID { get; set; }

        public string EmergencyContactPerson { get; set; }

        public string EmergenctContactMobile { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}