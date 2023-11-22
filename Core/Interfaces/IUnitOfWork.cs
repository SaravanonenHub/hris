using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        //IGenericRepository<T> RepositoryByTable<T>(string table) where T : BaseEntity;
        Task<int> Complete();
        //Task<object> GetEntityName(string tableName,int id) ;
    }
}