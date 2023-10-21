using Core.Entities.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications.EntriesSpec
{
    public class RequestSpecParams
    {
        public int? RequestId { get; set; }
        public int? TeamId { get; set; }
        public int? EmpId { get; set; }
        public ActionTaken? Status { get; set; }
        private string _search;
        public string Search
        {
            get { return _search; }
            set { _search = value.ToLower(); }
        }

    }
}