using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BaseInfoFluentConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseInformation
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> modelBuilder)
        {
            modelBuilder.Property(c => c.CreateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Property(c => c.LastModifiedDate).HasDefaultValueSql("getdate()");
            modelBuilder.Property(c => c.IsActive).HasDefaultValue("Y");
        }
    }
    public class BaseRequestFluentConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseRequest
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> modelBuilder)
        {
            modelBuilder.Property(c => c.RequestDate).HasDefaultValueSql("getdate()");
            modelBuilder.Property(c => c.RequestedBy).IsRequired();
        }
    }
   

}