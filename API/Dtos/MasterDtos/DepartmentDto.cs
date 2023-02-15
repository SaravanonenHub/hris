using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Masters;

namespace API.Dtos.MasterDtos
{
    public class DepartmentDto
    {
        public string DepartmentName { get; set; }
        public string ShortName { get; set; }
        public int DivisionId { get; set; }
    }
    public class DepartmentResponseDto
    {
        public string DepartmentName { get; set; }
        public string ShortName { get; set; }
        public DivisionDto division { get; set; }
    }
}