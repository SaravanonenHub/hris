using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.MasterDtos;

namespace API.Dtos.EmployeeDtos
{
    public class EmployeeRequestDto
    {
        [Required]
        public string EmployeeCode { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public string ImagePath { get; set; }
        public int BranchID { get; set; }
        public int DivisionID { get; set; }
        //public Division Division { get; set; }
        public int DepartmentID { get; set; }
        // public Department Department { get; set; }
        public int DesignationID { get; set; }
        // public Designation Designation { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public DateTime JoinDate { get; set; }
        [Required]
        public string EmailID { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string BloodGroup { get; set; }
        [Required]
        public string MartialStatus { get; set; }
        [Required]
        public string OptionalSaturday { get; set; }
        [Required]
        public string EmployeeNature { get; set; }
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int TeamRoleId { get; set; }

        public IFormFile EmpImage { get; set; }
    }
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string ImagePath { get; set; }
        public int BranchId { get; set; }
        public int DivisionId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int TeamId { get; set; }
        public int TeamRoleId { get; set; }
        public int RoleId { get; set; }
        public BranchDto Branch { get; set; }
        public DivisionDto Division { get; set; }
        //public Division Division { get; set; }
        public DepartmentResponseDto Department { get; set; }
        // public Department Department { get; set; }
        public DesignationDto Designation { get; set; }
        // public Designation Designation { get; set; }
        public string Qualification { get; set; }
        public string Status { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public int Age { get; set; }
        public DateTimeOffset JoinDate { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }
        public string MartialStatus { get; set; }
        public string OptionalSaturday { get; set; }
    }
}