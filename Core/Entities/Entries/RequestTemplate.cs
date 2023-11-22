using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Entries
{
    [Table("T_REQUEST_TEMPLATE")]
    public class RequestTemplate:BaseInformation
    {
        [Required]
        [MaxLength(15)]
        public string TemplateName { get; set; }
        [Required]
        [MaxLength(5)]
        public string TemplatePrefix { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        [Required]
        public string MainTableName { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string MethodName { get; set; }
       
        public ICollection<Request> Requests { get; set; }

    }
}
