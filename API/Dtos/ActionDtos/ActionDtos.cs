using API.Dtos.EntriesDtos;
using Core.Entities.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.ActionDtos
{
    public class ActionResponseDtos
    {
       

        public RequestResponseDto Request { get; set; }
        public string Action { get; set; }
        public string Comment { get; set; }
        public DateTime ActionDate { get; set; }
        public string ActionBy { get; set; } = "admin";
        private string _summmary;

        public string Summary
        {
            get 
            {
                switch (Action.ToLower())
                {
                    case "created":
                        return string.Format(ActionSummary.Created, Request.Type.TemplateName, ActionDate);
                    case "in approval":
                        return string.Format(ActionSummary.Inapproval, ActionBy);
                    case "in progress":
                        return string.Format(ActionSummary.Inprogress, ActionBy);
                    case "closed":
                        return string.Format(ActionSummary.Closed, ActionBy, Request.Status, Comment);
                    default:
                        return "Unknown action";
                }
            }
            set {
                _summmary = value; 
            }
        }


    }
}