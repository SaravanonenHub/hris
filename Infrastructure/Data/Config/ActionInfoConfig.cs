using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ActionInfoConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : ActionInformation
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> modelBuilder)
        {
            modelBuilder.Property(c => c.ActionDate).HasDefaultValueSql("getdate()");
            modelBuilder.Property(c => c.ActionBy).IsRequired();

        }
    }
}