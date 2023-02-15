using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Masters
{
    public enum ShiftType
    {
        Day,
        Night
    }
    [Table("T_SHIFT")]
    public class Shift : BaseInformation
    {
        [Required]
        [MaxLength(15)]
        public string ShiftName { get; set; }
        [Required]
        [MaxLength(5)]
        public string InTime { get; set; }
        [Required]
        [MaxLength(5)]
        public string OutTime { get; set; }
        [Required]
        [MaxLength(5)]
        public string WorkingHours { get; set; }
        [Required]
        [MaxLength(5)]
        public string Break { get; set; }
        [Required]
        [DefaultValue("N")]
        [MaxLength(5)]
        public string HasOT { get; set; }
        [Required]
        [DefaultValue("N")]
        [MaxLength(5)]
        public string HasSatOff { get; set; }
        [Required]

        private decimal _InTimeDbl;
        private decimal _OutTimeDbl;
        public ShiftType Type { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal InTimeDbl
        {
            get
            {
                return _InTimeDbl;
            }
            set
            {
                decimal InDbl = 0;
                InDbl = !string.IsNullOrEmpty(InTime)
                        ? InTime.Contains(":") ? Convert.ToDecimal(InTime.Replace(":", ".")) : 0
                        : 0;

                _InTimeDbl = InDbl;
            }


        }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OutTimeDbl
        {
            get
            {
                return _OutTimeDbl;
            }
            set
            {
                decimal OutDbl = 0;
                OutDbl = !string.IsNullOrEmpty(OutTime)
                        ? OutTime.Contains(":") ? Convert.ToDecimal(OutTime.Replace(":", ".")) : 0
                        : 0;

                _OutTimeDbl = OutDbl;
            }
        }


    }
}