using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class LeaveActionConfig : ActionInfoConfig<LeaveAction>
    {
        public override void Configure(EntityTypeBuilder<LeaveAction> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.ToTable("T_LEAVE_ACTIONS");
            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(c => c.Action).IsRequired();
        }
    }
}