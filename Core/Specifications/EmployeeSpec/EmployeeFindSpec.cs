using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications.EmployeeSpec
{
    public class EmployeeFindSpec
    {
        public int? Id { get; set; }
        public int? IdNotEqual { get; set; }
        public string EmpCode { get; set; }
    }
}