using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities.Masters
{
    public enum PolicyType
    {
        Yearly,
        Monthly
    }
    public class LeavePolicy : BaseInformation
    {
        public string PolicyName { get; set; }
        public string ShortName { get; set; }
        [JsonConverter(typeof(PolicyTypeConverter))]
        public PolicyType Type { get; set; }
        public IReadOnlyList<LeavePolicyDetails> LeavePolicyDetails { get; set; }

    }
    [Table("T_LEAVE_TYPE")]
    public class LeaveType : BaseInformation
    {
        [Required]
        [MaxLength(20)]
        public string LeaveName { get; set; }
        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }
        public IReadOnlyList<LeavePolicyDetails> LeavePolicyDetails { get; set; }
    }
    public class LeavePolicyDetails:BaseEntity
    {

        public int LeavePolicyId { get; set; }
        public LeavePolicy LeavePolicy { get; set; }
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public int Day { get; set; }
    }
    public class PolicyTypeConverter : JsonConverter<PolicyType>
    {
        public override PolicyType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string value = reader.GetString();
                if (Enum.TryParse(value, true, out PolicyType policyType))
                {
                    return policyType;
                }
            }

            throw new JsonException($"Unable to convert JSON value to {nameof(PolicyType)}");
        }

        public override void Write(Utf8JsonWriter writer, PolicyType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

}
