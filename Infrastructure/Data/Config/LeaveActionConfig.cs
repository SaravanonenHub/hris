using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Actions;
using Core.Entities.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class LeaveActionConfig : ActionInfoConfig<ActionHistory>
    {
        public override void Configure(EntityTypeBuilder<ActionHistory> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.ToTable("T_ACTION_HISTORY");
            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(c => c.Action).IsRequired();
            modelBuilder
                .HasOne(c => c.Request)
                .WithOne()
                .HasForeignKey<Request>(c => c.Id)
                .IsRequired();
        }
    }
}