using Core.Entities.Actions;
using Core.Entities.Entries;
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
        public string EmployeeCode { get; set; }
        public string Status { get; set; }
        public string EmpIds { get; set; }
        public string RequestIds { get; set; }
        private string _search;
        public string Search
        {
            get { return _search; }
            set { _search = value.ToLower(); }
        }

    }
    public class RequestParams
    {
        public int? RequestId { get; set; }
        public string RequestType { get; set; }
        public int? EmpId { get; set; }
        public string Status { get; }
        private string _search;
        public string Search
        {
            get { return _search; }
            set { _search = value.ToLower(); }
        }

    }
}