using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.MasterDtos;

namespace API.Dtos.EmployeeDtos
{
    public class EmployeeCommonDto
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string DisplayName { get; set; }
        public DepartmentDto Department { get; set; }
        public string TeamRole { get; set; }
    }
}