using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Actions;
using Core.Entities.Entries;
using Core.Entities.Notify;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class LeaveFluentConfig : BaseInfoFluentConfig<Leave>
    {
        public override void Configure(EntityTypeBuilder<Leave> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.ToTable("T_LEAVE");
            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(c => c.FromDate).IsRequired();
            modelBuilder.Property(c => c.ToDate).IsRequired();
            modelBuilder.Property(c => c.Days).IsRequired();
            modelBuilder.Property(c => c.LeaveType).IsRequired().HasMaxLength(15); ;
            modelBuilder.Property(c => c.Session).IsRequired().HasMaxLength(10);
            modelBuilder.HasMany<LeaveAction>(c => c.Actions).WithOne(o => o.Leave).IsRequired().OnDelete(DeleteBehavior.NoAction);
            // modelBuilder.HasOne<NotifyProps>(c => c.Notify).WithOne().IsRequired().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Property(s => s.Status)
             .HasConversion(
                 o => o.ToString(),
                 o => (ActionTaken)Enum.Parse(typeof(ActionTaken), o)
             ).IsRequired().HasMaxLength(15);
        }
    }
}