using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly HRISContext _context;
        public GenericRepository(HRISContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<T> GetEntityWithAllIncludesSpec(ISpecification<T> spec, ISpecification<T> spec2)
        {
            return await ApplySpecificationCombine(spec, spec2).FirstOrDefaultAsync();
        }
        public IQueryable<T> GetEntityWithSpecNoTrack(ISpecification<T> spec)
        {
            return ApplySpecificationNoTrack(spec);
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
        private IQueryable<T> ApplySpecificationCombine(ISpecification<T> spec, ISpecification<T> spec2)
        {
            return SpecificationEvaluator<T>.GetQueryCombine(_context.Set<T>().AsQueryable(), spec, spec2);
        }
        private IQueryable<T> ApplySpecificationNoTrack(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsNoTracking().AsQueryable(), spec);
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            // _context.Entry<T>(entity).State = EntityState.Detached;
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetByIdWithoutTrack(int id)
        {
            return _context.Set<T>()?.Where(o => o.Id == id).AsNoTracking().AsQueryable();
        }


    }
}