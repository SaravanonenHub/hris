using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Notify;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class NotifyConfig : BaseInfoFluentConfig<NotifyProps>
    {
        public override void Configure(EntityTypeBuilder<NotifyProps> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.ToTable("T_NOTIFICATION");
            modelBuilder.Property(c => c.Type).IsRequired().HasMaxLength(15);
            modelBuilder.HasOne<Team>(c => c.Team).WithMany(o => o.Notifications).IsRequired(false);
            modelBuilder.HasOne<TeamRole>(c => c.TeamRole).WithMany(o => o.Notifications).IsRequired(false);

        }
    }
}