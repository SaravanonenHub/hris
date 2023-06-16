using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Actions;
using Core.Entities.Employees;
using Core.Entities.Entries;
using Core.Entities.Identity;
using Core.Entities.Masters;
using Core.Entities.Notify;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class HRISContext : DbContext
    {
        public HRISContext(DbContextOptions<HRISContext> options) : base(options)
        {
        }

        public DbSet<Division> Divisions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamRole> TeamRoles { get; set; }
        public DbSet<TeamDetails> TeamDetails { get; set; }
        //public DbSet<TeamEmployee> TeamEmployees { get; set; }
        public DbSet<UserLevel> UserLevels { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeShiftDetails> EmployeeShiftDetails { get; set; }
        public DbSet<EmployeePersonalInfo> EmployeePersonalInfos { get; set; }
        public DbSet<EmployeeExperienceInfo> EmployeeExperienceInfos { get; set; }

        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<Leave> Leave { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<LeaveAction> LeaveActions { get; set; }
        public DbSet<NotifyProps> NotifyProps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(BaseInformation).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(BaseInformation.CreateDate))
                    .HasDefaultValueSql("getdate()");
                modelBuilder
                   .Entity(entityType.ClrType)
                   .Property(nameof(BaseInformation.LastModifiedDate))
                   .HasDefaultValueSql("getdate()");
                modelBuilder
                  .Entity(entityType.ClrType)
                  .Property(nameof(BaseInformation.IsActive))
                  .HasDefaultValue("Y");
            }
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseInfoFluentConfig<BaseInformation>).Assembly);
            modelBuilder.Entity<EmployeeExperienceInfo>()
                        .HasOne(o => o.Employee)
                        .WithOne(d => d.EmployeeExperienceInfo)
                        .HasForeignKey<EmployeeExperienceInfo>(k => k.EmployeeID).IsRequired();
            modelBuilder.Entity<EmployeePersonalInfo>()
                        .HasOne(o => o.Employee)
                        .WithOne(d => d.EmployeePersonalInfo)
                        .HasForeignKey<EmployeePersonalInfo>(k => k.EmployeeID).IsRequired();
            modelBuilder.Entity<TeamRole>()
                        .Property(c => c.HasApprovalAuth).HasDefaultValue("N").HasColumnType("char(1)").IsRequired();

            modelBuilder.ApplyConfiguration(new EmployeeFluentConfig());
            modelBuilder.ApplyConfiguration(new ShiftFluentConfig());
            modelBuilder.ApplyConfiguration(new LeaveFluentConfig());
            modelBuilder.ApplyConfiguration(new LeaveActionConfig());
            modelBuilder.ApplyConfiguration(new NotifyConfig());
            modelBuilder.ApplyConfiguration(new TeamDetailsFluentConfig());
        }
    }
}