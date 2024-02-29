using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<object> GetObjByIdAsync(int id);
        IQueryable<T> GetQueryByIdTrack(int id, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetByIdWithoutTrack(int id);
        IQueryable<T> GetEntityWithSpecNoTrack(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<T> GetEntityWithAllIncludesSpec(ISpecification<T> spec, ISpecification<T> spec2);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}