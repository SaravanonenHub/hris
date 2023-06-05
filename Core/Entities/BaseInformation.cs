using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{

    public class BaseInformation : BaseEntity
    {

        public DateTime CreateDate { get; set; }
        [MaxLength(15)]
        public string CreatedBy { get; set; } = "admin";
        [MaxLength(5)]
        public string IsActive { get; set; }
        public DateTime LastModifiedDate { get; set; }
        [MaxLength(15)]
        public string LastModifiedBy { get; set; } = "admin";
    }
    public class ActionInformation : BaseEntity
    {

        public DateTime ActionDate { get; set; }
        [MaxLength(15)]
        public string ActionBy { get; set; } = "admin";

    }
}