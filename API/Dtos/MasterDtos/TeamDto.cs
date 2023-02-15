using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;

namespace API.Dtos.MasterDtos
{
    public class TeamDto
    {
        [Required]
        public string TeamName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        public IReadOnlyList<TeamDetailsDto> TeamDetails { get; set; }
    }
    public class TeamDetailsDto
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}