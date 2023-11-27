using Core.Entities.Employees;
using Core.Entities.Entries;
using Core.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class HRISMasterContextSeed
    {
        public static async Task SeedAsync(HRISContext context)
        {
            if (!context.Branches.Any())
            {
                var branchData = File.ReadAllText("../Infrastructure/Data/SeedData/branch.json");
                var branches = JsonSerializer.Deserialize<List<Branch>>(branchData);
                context.Branches.AddRange(branches);
            }
            if (!context.Divisions.Any())
            {
                var divisionData = File.ReadAllText("../Infrastructure/Data/SeedData/division.json");
                var divisons = JsonSerializer.Deserialize<List<Division>>(divisionData);
                context.Divisions.AddRange(divisons);
            }
            
            if (!context.Designations.Any())
            {
                var designationData = File.ReadAllText("../Infrastructure/Data/SeedData/designation.json");
                var designations = JsonSerializer.Deserialize<List<Designation>>(designationData);
                context.Designations.AddRange(designations);
            }
            if (!context.TeamRoles.Any())
            {
                var roleData = File.ReadAllText("../Infrastructure/Data/SeedData/role.json");
                var roles = JsonSerializer.Deserialize<List<TeamRole>>(roleData);
                context.TeamRoles.AddRange(roles);
            }
            if (!context.LeaveType.Any())
            {
                var leaveTypeData = File.ReadAllText("../Infrastructure/Data/SeedData/leaveType.json");
                var leaveTypes = JsonSerializer.Deserialize<List<LeaveType>>(leaveTypeData);
                context.LeaveType.AddRange(leaveTypes);
            }
            if (!context.LeavePolicies.Any())
            {
                var leavePolicyData = File.ReadAllText("../Infrastructure/Data/SeedData/leavePolicy.json");
                var leavePolicies = JsonSerializer.Deserialize<List<LeavePolicy>>(leavePolicyData);
                context.LeavePolicies.AddRange(leavePolicies);
            }
            if (!context.RequestTemplates.Any())
            {
                var requestTemplateData = File.ReadAllText("../Infrastructure/Data/SeedData/requestTemplate.json");
                var requestTemplates = JsonSerializer.Deserialize<List<RequestTemplate>>(requestTemplateData);
                context.RequestTemplates.AddRange(requestTemplates);
            }
            
            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
    public class HRISDetailContextSeed
    {
        public static async Task SeedAsync(HRISContext context)
        {
            
            if (!context.Departments.Any())
            {
                var departmentData = File.ReadAllText("../Infrastructure/Data/SeedData/department.json");
                var departments = JsonSerializer.Deserialize<List<Department>>(departmentData);
                context.Departments.AddRange(departments);
            }
            
           
            if (!context.LeavePolicyDetails.Any())
            {
                var leavePolicyDetailData = File.ReadAllText("../Infrastructure/Data/SeedData/leavePolicyDetail.json");
                var leavePoliciyDetails = JsonSerializer.Deserialize<List<LeavePolicyDetails>>(leavePolicyDetailData);
                context.LeavePolicyDetails.AddRange(leavePoliciyDetails);
            }
            if (!context.Employees.Any())
            {
                var employeeData = File.ReadAllText("../Infrastructure/Data/SeedData/employee.json");
                var employees = JsonSerializer.Deserialize<List<Employee>>(employeeData);
                context.Employees.AddRange(employees);
            }
            
            
            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
    public class HRISDetailsChildContextSeed
    {
        public static async Task SeedAsync(HRISContext context)
        {
            if (!context.Teams.Any())
            {
                var teamData = File.ReadAllText("../Infrastructure/Data/SeedData/team.json");
                var teams = JsonSerializer.Deserialize<List<Team>>(teamData);
                await context.Teams.AddRangeAsync(teams);
                if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
            }
            if (!context.TeamDetails.Any())
            {
                var teamDetailsData = File.ReadAllText("../Infrastructure/Data/SeedData/teamDetails.json");
                var teamDetails = JsonSerializer.Deserialize<List<TeamDetails>>(teamDetailsData);
                context.TeamDetails.AddRange(teamDetails);
                if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
            }
            
        }
    }
}
