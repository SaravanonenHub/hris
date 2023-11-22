using Core.Entities.Actions;
using Core.Entities.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Entries
{
    
    public class Request:BaseRequest
    {
        public string RequestId { get; set; }
        public Employee Employee { get; set; }
        public int TypeId { get; set; }
        public RequestTemplate Type { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public char CancellationStatus { get; set; }
        public ICollection<ActionHistory> Actions { get; set; }
        //List<ActionHistory> Actions { get; set; }

        //public Leave Leave { get; set; }


    }
    
 
}
