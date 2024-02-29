using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Employees;
using Core.Entities.Entries;
using Core.Interfaces;
using Core.Interfaces.IEntries;
using Core.Specifications;
using Core.Specifications.EntriesSpec;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Services.EntriesRepo
{
    public class LeaveService : ILeaveService
    {
        private readonly IUnitOfWork _unitofWork;

        public LeaveService(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        //public string GetName(string tableName)
        //{
        //    return _unitofWork.GetEntityName(tableName);
        //}
        public async Task<bool> AlreadyExists(int id,int empID, DateTime fDate, DateTime TDate)
        {
            if(id == 0)
            {
                var spec = new BaseSpecification<Leave>(c => c.Request.Employee.Id == empID && c.FromDate >= fDate && c.ToDate <= TDate);
                //var spec = new BaseSpecification<Leave>(c => c.FromDate >= fDate && c.ToDate <= TDate);
                var exists = await _unitofWork.Repository<Leave>().ListAsync(spec);
                if (exists.Count == 0) return true;
            }
            else
            {
                var spec = new BaseSpecification<Leave>(c => c.Request.Employee.Id == empID && c.Id != id && c.FromDate >= fDate && c.ToDate <= TDate);
                //var spec = new BaseSpecification<Leave>(c => c.FromDate >= fDate && c.ToDate <= TDate);
                var exists = await _unitofWork.Repository<Leave>().ListAsync(spec);
                if (exists.Count == 0) return true;
            }
           
            return false;

        }

        public Task<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }
        
        
        public async Task<IReadOnlyList<Leave>> MyLeaveRequests(RequestsByTeamSpecification specification)
        {
            return await _unitofWork.Repository<Leave>().ListAsync(specification);
        }

        public async Task<Leave> GetRequestById(RequestsByTeamSpecification specification)
        {
            return await _unitofWork.Repository<Leave>().GetEntityWithSpec(specification);
        }
        public async Task<Leave> GetLeavebyRequestId(LeaveSpecification specification)
        {
            return await _unitofWork.Repository<Leave>().GetEntityWithSpec(specification);
        }
        public IQueryable<Leave> GetRequestByIdNoTrack(RequestsByTeamSpecification specification)
        {
            return _unitofWork.Repository<Leave>().GetEntityWithSpecNoTrack(specification);
        }

        public async Task<Leave> SubmitLeave(Leave _leave)
        {
            _unitofWork.Repository<Leave>().Add(_leave);

            var result = await _unitofWork.Complete();
            if (result <= 0) return null;
            // return employee
            return _leave;
        }

        public async Task<Leave> UpdateLeave(Leave _leave)
        {
            _unitofWork.Repository<Leave>().Update(_leave);
            var result = await _unitofWork.Complete();

            if (result <= 0) return null;

            // return order
            return _leave;
        }

        public Task<IReadOnlyList<Leave>> RequesttoApproval(string empId)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestTemplate> GetTemplatebyId(int id)
        {
            var result = await _unitofWork.Repository<RequestTemplate>().GetByIdAsync(id);
            if (result == null) return null;
            return result;
        }

        public Leave GetLeaveById<T>(int id) where T : BaseEntity
        {
            var query =  _unitofWork.Repository<Leave>().GetQueryByIdTrack(id, e => e.Request);
            query = query.Include(x => x.Request).AsNoTracking();
            //query = query.Include(_ => _.Request).ThenInclude(x => x.Employee).AsNoTracking();
            var leave =  query.FirstOrDefault(x => x.Id == id);
            if(leave == null) return null;
            return leave;
        }
    }
}