using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.EmployeeDtos
{
    public class EmpExperianceRequestDto
    {
        [Required]
        public int EmployeeID { get; set; }
        public int PastExp_Year { get; set; }
        public int PastExp_Month { get; set; }
        public int CurrentExp_Year { get; set; }
        public int CurrentExp_Month { get; set; }
    }
    public class EmpExperianceResponseDto
    {
        public EmployeeCommonDto Employee { get; set; }
        public int PastExp_Year { get; set; }
        public int PastExp_Month { get; set; }
        public int CurrentExp_Year { get; set; }
        public int CurrentExp_Month { get; set; }
    }
}