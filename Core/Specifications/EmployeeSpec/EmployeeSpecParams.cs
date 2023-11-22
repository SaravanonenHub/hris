using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications.EmployeeSpec
{
    public class EmployeeSpecParams
    {
        // private const int MaxPageSize = 50;
        // public int PageIndex { get; set; } = 1;
        // private int _pageSize = 6;
        // public int PageSize
        // {
        //     get { return _pageSize; }
        //     set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }

        // 
        public int? Id { get; set; }
        public string Code {get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
        public string EmployeeNature { get; set; }
        // public List<int> DepartmentIds { get; set; }
        public string DepartmentIds { get; set; }
        public string DivisionIds { get; set; }
    }
}