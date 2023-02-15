using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Masters;

namespace Core.Entities.Employees
{
    [Table("T_EMPLOYEE_SHIFTS")]
    public class EmployeeShiftDetails : BaseEntity
    {
        public Employee Employee { get; set; }
        [Required]
        public int ShiftID { get; set; }
        public Shift Shift { get; set; }
    }
}