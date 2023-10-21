using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Identity;
using Core.Specifications.EmployeeSpec;

namespace Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeById(int Id, string EmpCode = null);
        IQueryable<Employee> GetEmployeeByIdNoTrack(int Id);
        Task<IReadOnlyList<Employee>> GetEmployeesAsync(EmployeeWithFilterSpec spec);
        Task<IReadOnlyList<EmployeeNature>> GetEmployeeNatureAsync();
        //Task<IReadOnlyList<Employee>> GetEmployeeDetailsAsync();
        Task<Employee> CheckEmployeeonUpdate(string name, int id);
        Task<Employee> CreateEmployee(Employee emp);
        // Task<AppUser> CreateUser(AppUser user);
        Task<Employee> UpdateEmployee(Employee emp);
        Task<EmployeePersonalInfo> GetEmployeePersonalById(int Id, string EmpCode = null);
        IQueryable<EmployeePersonalInfo> GetEmployeePersonalByIdNoTrack(int Id);
        Task<IReadOnlyList<EmployeePersonalInfo>> GetEmployeesPersonalAsync();
        Task<EmployeePersonalInfo> CreateEmployeePersonal(EmployeePersonalInfo emp);
        Task<EmployeePersonalInfo> UpdateEmployeePersonal(EmployeePersonalInfo emp);

        Task<EmployeeExperienceInfo> GetEmployeeExperienceById(int Id, string EmpCode = null);
        IQueryable<EmployeeExperienceInfo> GetEmployeeExperienceByIdNoTrack(int Id);
        Task<IReadOnlyList<EmployeeExperienceInfo>> GetEmployeeExperiencesAsync();
        Task<EmployeeExperienceInfo> CreateEmployeeExperience(EmployeeExperienceInfo emp);
        Task<EmployeeExperienceInfo> UpdateEmployeeExperience(EmployeeExperienceInfo emp);

        // Task<Employee> GetEmployeeExperienceById(int Id, string EmpCode = null);
        // IQueryable<Employee> GetEmployeeExperienceByIdNoTrack(int Id);
        // Task<IReadOnlyList<Employee>> GetEmployeesExperienceAsync();
        // Task<EmployeeExperienceInfo> CreateEmployeeExperience(EmployeeExperienceInfo emp);
        // Task<EmployeeShiftDetails> CreateEmployeeShift(EmployeeShiftDetails emp);

    }
}