using Core.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Config
{
    public class LeavePolicyConfig:BaseInfoFluentConfig<LeavePolicy>
    {
        public override void Configure(EntityTypeBuilder<LeavePolicy> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.ToTable("T_LEAVE_POLICY");
            modelBuilder.Property(x => x.PolicyName).HasMaxLength(25).IsRequired();
            modelBuilder.Property(x => x.ShortName).HasMaxLength(10).IsRequired();
            modelBuilder.HasMany<LeavePolicyDetails>(z => z.LeavePolicyDetails).WithOne(x => x.LeavePolicy).IsRequired().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Property(s => s.Type)
           .HasConversion(
               o => o.ToString(),
               o => (PolicyType)Enum.Parse(typeof(PolicyType), o)
           ).IsRequired().HasMaxLength(15);
        }
    }
    public class LeavePolicyDetailsConfig :IEntityTypeConfiguration<LeavePolicyDetails>
    {
        public  void Configure(EntityTypeBuilder<LeavePolicyDetails> modelBuilder)
        {

            modelBuilder.ToTable("T_LEAVE_POLICY_DETAIL");
            modelBuilder.HasOne<LeaveType>(z => z.LeaveType).WithMany(x => x.LeavePolicyDetails).IsRequired().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Property(x => x.Day).IsRequired();

        }
    }
}
