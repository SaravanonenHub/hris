using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ShiftFluentConfig : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> modelBuilder)
        {
            modelBuilder.Property(c => c.InTimeDbl).HasPrecision(18, 2);
            modelBuilder.Property(c => c.OutTimeDbl).HasPrecision(18, 2);
            modelBuilder.Property(c => c.HasSatOff).HasDefaultValue("N");
            modelBuilder.Property(c => c.HasOT).HasDefaultValue("N");
        }
    }
}