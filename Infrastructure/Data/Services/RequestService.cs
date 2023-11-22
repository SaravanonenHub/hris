using Core.Entities.Entries;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitofWork;

        public RequestService(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<Request> CreateRequest(Request req)
        {
             _unitofWork.Repository<Request>().Add(req);

            var result = await _unitofWork.Complete();
            if (result <= 0) return null;
            // return employee
            return req;
        }
        public async Task<Request> UpdateRequest(Request _req)
        {
            _unitofWork.Repository<Request>().Update(_req);
            var result = await _unitofWork.Complete();

            if (result <= 0) return null;

            // return order
            return _req;
        }
        public async Task<Request> GetRequest(int id)
        {
            var spec = new RequestSpec();
            return await _unitofWork.Repository<Request>().GetEntityWithSpec(spec);
        }
        //public async Task<object> GetEntity(string tableName, int id)
        //{
        //    return await _unitofWork.GetEntityName(tableName, id);
        //}
        public async Task<IReadOnlyList<Request>> GetRequests(RequestSpec specification)
        {
            return await _unitofWork.Repository<Request>().ListAsync(specification);
        }
    }
}
