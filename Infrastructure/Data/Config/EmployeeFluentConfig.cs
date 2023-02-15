using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Masters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class EmployeeFluentConfig : BaseInfoFluentConfig<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> modelBuilder)
        {
            base.Configure(modelBuilder);
            modelBuilder.ToTable("T_EMPLOYEE");
            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(c => c.EmployeeCode).IsRequired().HasMaxLength(10);
            modelBuilder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Property(c => c.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Property(c => c.Qualification).IsRequired().HasMaxLength(20);
            // modelBuilder.Property(c => c.Status).IsRequired().HasMaxLength(10);
            modelBuilder.Property(c => c.BirthDate).IsRequired();
            modelBuilder.Property(c => c.JoinDate).IsRequired();
            modelBuilder.Property(c => c.EmailID).IsRequired();
            modelBuilder.Property(c => c.EmployeeNature).IsRequired();
            // modelBuilder.Property(c => c.MartialStatus).IsRequired();
            modelBuilder.Property(c => c.BloodGroup).IsRequired();
            modelBuilder.Property(c => c.CreatedBy).IsRequired().HasMaxLength(10);

            modelBuilder.HasOne<Branch>(c => c.Branch).WithMany().IsRequired();
            modelBuilder.HasOne<Division>(c => c.Division).WithMany().IsRequired().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.HasOne<Department>(c => c.Department).WithMany().IsRequired().OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.HasOne<Designation>(c => c.Designation).WithMany().IsRequired().OnDelete(DeleteBehavior.NoAction); ;
            modelBuilder.HasOne<Team>(c => c.Team).WithMany().IsRequired().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.HasOne<TeamRole>(c => c.TeamRole).WithMany().IsRequired().OnDelete(DeleteBehavior.NoAction);
            // modelBuilder.HasOne<EmployeeNature>(c => c.EmployeeNature).WithMany().IsRequired().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (EmployeeStatus)Enum.Parse(typeof(EmployeeStatus), o)
                ).IsRequired().HasMaxLength(15);
            modelBuilder.Property(s => s.MartialStatus)
                .HasConversion(
                    o => o.ToString(),
                    o => (EmployeeMartialStatus)Enum.Parse(typeof(EmployeeMartialStatus), o)
                ).IsRequired().HasMaxLength(15);
            modelBuilder.Property(s => s.Gender)
               .HasConversion(
                   o => o.ToString(),
                   o => (EmployeeGender)Enum.Parse(typeof(EmployeeGender), o)
               ).IsRequired().HasMaxLength(15);
            // modelBuilder.HasOne<EmployeePersonalInfo>(g => g.EmployeePersonalInfo).WithOne(d => d.Employee).OnDelete(DeleteBehavior.Cascade);
            // modelBuilder.HasOne<EmployeeExperienceInfo>(g => g.EmployeeExperienceInfo).WithOne(d => d.Employee).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.HasMany<EmployeeShiftDetails>(g => g.EmployeeShiftDetails).WithOne(d => d.Employee).OnDelete(DeleteBehavior.Cascade);
        }
    }
}