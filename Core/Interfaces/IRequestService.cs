using Core.Entities.Entries;
using Core.Specifications;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRequestService
    {
        Task<IReadOnlyList<Request>> GetRequests(RequestSpec specification);
        Task<Request> GetRequest(int id);
        Task<Request> CreateRequest(Request req);
        Task<Request> UpdateRequest(Request req);
        //Task<object> GetEntity(string tableName, int id);
    }
}
