using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Entities.Masters;
using Core.Entities.Notify;

namespace Core.Entities.Employees
{
    public static class EmployeeStatus
    {
        public static readonly string Live = "Live";
        public static readonly string NotWorking = "Not Working";


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
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int DivisionId { get; set; }
        public Division Division { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        public LeavePolicy LeavePolicy { get; set; }
        public int LeavePolicyId { get; set; }
        [MaxLength(30)]
        public string Qualification { get; set; }
        public string Status { get; set; } = EmployeeStatus.Live;
        public DateTimeOffset BirthDate { get; set; } = DateTimeOffset.Now;
        public int Age { get; set; }
        public DateTimeOffset JoinDate { get; set; } = DateTimeOffset.Now;
        [MaxLength(100)]
        public string EmailID { get; set; }
        [JsonConverter(typeof(EmployeeGenderConverter))]
        public EmployeeGender Gender { get; set; }
        [MaxLength(15)]
        public string BloodGroup { get; set; }
        [JsonConverter(typeof(EmployeeMartialStatusConverter))]
        public EmployeeMartialStatus MartialStatus { get; set; }
        public string EmployeeNature { get; set; }
        [MaxLength(1)]
        public string OptionalSaturday { get; set; }
        // public int UserLevelID { get; set; }
        // public Team Team { get; set; }
        public string TeamRole { get; set; }
        public EmployeePersonalInfo EmployeePersonalInfo { get; set; }
        public EmployeeExperienceInfo EmployeeExperienceInfo { get; set; }
        public List<EmployeeShiftDetails> EmployeeShiftDetails { get; set; }
        public List<NotifyProps> Notifications { get; set; }
    }

    public class EmployeeGenderConverter : JsonConverter<EmployeeGender>
    {
        public override EmployeeGender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string value = reader.GetString();
                if (Enum.TryParse(value, true, out EmployeeGender employeeGender))
                {
                    return employeeGender;
                }
            }

            throw new JsonException($"Unable to convert JSON value to {nameof(PolicyType)}");
        }

        public override void Write(Utf8JsonWriter writer, EmployeeGender value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
    public class EmployeeMartialStatusConverter : JsonConverter<EmployeeMartialStatus>
    {
        public override EmployeeMartialStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string value = reader.GetString();
                if (Enum.TryParse(value, true, out EmployeeMartialStatus employeeMartialStatus))
                {
                    return employeeMartialStatus;
                }
            }

            throw new JsonException($"Unable to convert JSON value to {nameof(PolicyType)}");
        }

        public override void Write(Utf8JsonWriter writer, EmployeeMartialStatus value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}