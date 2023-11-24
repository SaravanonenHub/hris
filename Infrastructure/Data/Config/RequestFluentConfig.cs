using Core.Entities.Actions;
using Core.Entities.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class RequestFluentConfig : BaseRequestFluentConfig<Request>
    {
        public override void Configure(EntityTypeBuilder<Request> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.ToTable("T_REQUEST");
            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(c => c.RequestId).IsRequired();
            //modelBuilder.Property(c => c.Status).IsRequired();
            //modelBuilder.Property(c => c.CancellationStatus).HasDefaultValue("N").IsRequired();
            modelBuilder
                .HasOne(p => p.Type)
                .WithMany(d => d.Requests)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .HasOne<Leave>()
                .WithOne(r => r.Request)
                .HasForeignKey<Leave>(c => c.RequestId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
           modelBuilder
                .HasMany<ActionHistory>(p => p.Actions)
                .WithOne(g => g.Request)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
