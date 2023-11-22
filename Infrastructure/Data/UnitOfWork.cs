using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Entries;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRISContext _context;
        private Hashtable _repositories;
        public UnitOfWork(HRISContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
        //public  Task<object> GetEntityName(string tableName, int id)
        //{
        //    foreach (var entityType in _context.Model.GetEntityTypes())
        //    {
        //        var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
        //        if (tableNameAnnotation != null && tableNameAnnotation.Value.ToString() == tableName)
        //        {
        //            var repositoryType = typeof(UnitOfWork); // Replace YourClassName with the actual name of the class containing the Repository method
        //            var repositoryMethod = repositoryType.GetMethod("Repository").MakeGenericMethod(entityType.ClrType);
        //            //var repository = repositoryMethod.Invoke(this, null);

        //            var genericRepositoryType = repositoryMethod.MakeGenericMethod(entityType.ClrType);
        //            var repository = Activator.CreateInstance(genericRepositoryType.MemberType, _context);

        //            var entityName = entityType.ClrType;
        //            // Assuming GetByIdAsync is a method in your IGenericRepository<TEntity> interface
        //            var getByIdMethod = repository.GetType().GetMethod("GetObjByIdAsync");
        //            //var result = getByIdMethod.Invoke(repository, new object[] { id });
        //            var resultTask = getByIdMethod.Invoke(repository, new object[] { 11 }) as Task<object>;

        //            return resultTask;
        //            //object result = resultTask.GetType().GetProperty("Result")?.GetValue(resultTask);
        //            //return result;
        //        }
        //    }

        //    return null;
        //}
    }
    
}