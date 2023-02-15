using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Core.Entities.Masters;

namespace Core.Entities.Employees
{
    public enum EmployeeStatus
    {
        [EnumMember(Value = "Live")]
        Live,
        [EnumMember(Value = "Not Working")]
        NotWorking,

    }
    public enum EmployeeGender
    {
        Male,
        Female
    }
    public enum EmployeeMartialStatus
    {
        Single,
        Married
    }
    public class EmployeeNature : BaseEntity
    {
        public string Nature { get; set; }
    }
    public class Employee : BaseInformation
    {
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string ImagePath { get; set; }
        // public int BranchID { get; set; }
        public Branch Branch { get; set; }
        // public int DivisionID { get; set; }
        public Division Division { get; set; }
        // public int DepartmentID { get; set; }
        public Department Department { get; set; }
        // public int DesignationID { get; set; }
        public Designation Designation { get; set; }
        [MaxLength(30)]
        public string Qualification { get; set; }
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Live;
        public DateTimeOffset BirthDate { get; set; } = DateTimeOffset.Now;
        public int Age { get; set; }
        public DateTimeOffset JoinDate { get; set; } = DateTimeOffset.Now;
        [MaxLength(100)]
        public string EmailID { get; set; }
        public EmployeeGender Gender { get; set; }
        [MaxLength(15)]
        public string BloodGroup { get; set; }
        public EmployeeMartialStatus MartialStatus { get; set; }
        public string EmployeeNature { get; set; }
        [MaxLength(1)]
        public string OptionalSaturday { get; set; }
        // public int UserLevelID { get; set; }
        public Team Team { get; set; }
        public TeamRole TeamRole { get; set; }
        public EmployeePersonalInfo EmployeePersonalInfo { get; set; }
        public EmployeeExperienceInfo EmployeeExperienceInfo { get; set; }
        public List<EmployeeShiftDetails> EmployeeShiftDetails { get; set; }
    }
}