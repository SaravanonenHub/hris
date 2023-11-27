using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class TeamDetailsFluentConfig : BaseInfoFluentConfig<TeamDetails>
    {
        public override void Configure(EntityTypeBuilder<TeamDetails> modelBuilder)
        {
            base.Configure(modelBuilder);
            //modelBuilder.HasOne(c => c.Role)
            //    .WithMany().IsRequired().HasForeignKey(c => c.RoleId)
            //    .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            //modelBuilder.HasOne(c => c.Employee)
            //   .WithMany().IsRequired().HasForeignKey(c => c.EmployeeId)
            //   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

            //modelBuilder.HasOne(c => c.Team)
            //  .WithMany().IsRequired().HasForeignKey(c => c.TeamId)
            //  .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);

        }
    }
}