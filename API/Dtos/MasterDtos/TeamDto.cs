using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.EmployeeDtos;
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
        public string RoleName { get; set; }
        public TeamRole Role { get; set; }
    }
    public class TeamResponseDto
    {
        [Required]
        public string TeamName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public EmployeeCommonDto Manager { get; set; }
        public EmployeeCommonDto TeamLeader { get; set; }
        public IReadOnlyList<EmployeeCommonDto> Members { get; set; }
        [Required]
        public IReadOnlyList<TeamDetailsResponseDto> TeamDetails { get; set; }
    }
    public class TeamDetailsResponseDto
    {
        public int Id { get; set; }
        [Required]
        public EmployeeCommonDto Employee { get; set; }
        [Required]
        public TeamRole Role { get; set; }


    }
}