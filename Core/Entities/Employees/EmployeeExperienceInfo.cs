using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Employees
{
    [Table("T_EMPLOYEE_EXPERIANCE")]
    public class EmployeeExperienceInfo : BaseInformation
    {
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public int PastExp_Year { get; set; }
        public int PastExp_Month { get; set; }
        public int CurrentExp_Year { get; set; }
        public int CurrentExp_Month { get; set; }
    }
}