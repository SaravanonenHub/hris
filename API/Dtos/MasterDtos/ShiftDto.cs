using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Masters;

namespace API.Dtos.MasterDtos
{
    public class ShiftDto
    {
        [Required]
        public string ShiftName { get; set; }
        [Required]
        public string InTime { get; set; }
        [Required]
        public string OutTime { get; set; }
        [Required]
        public string WorkingHours { get; set; }
        [Required]
        public string Break { get; set; }
        [Required]
        public string HasOT { get; set; }
        [Required]
        public string HasSatOff { get; set; }
        [Required]
        public ShiftType Type { get; set; }
    }
}