using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Identity;
using Core.Interfaces;
using Core.Specifications.EmployeeSpec;

namespace Infrastructure.Data.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region EmployeeBasic
        public async Task<Employee> CheckEmployeeonUpdate(string empCode, int id)
        {
            var param = new EmployeeFindSpec();
            param.EmpCode = empCode;
            param.IdNotEqual = id;
            var spec = new EmployeeWithFilterSpec(id,param);
            return await _unitOfWork.Repository<Employee>().GetEntityWithSpec(spec);
        }
        public async Task<Employee> GetEmployeeById(int Id, string EmpCode = null)
        {
            var param = new EmployeeFindSpec();
            //param.Id = null;
            param.EmpCode = EmpCode;
            //param.IdNotEqual = 0;
            var spec = new EmployeeWithFilterSpec(Id, param);
            return await _unitOfWork.Repository<Employee>().GetEntityWithSpec(spec);
        }

        public IQueryable<Employee> GetEmployeeByIdNoTrack(int Id)
        {
            return _unitOfWork.Repository<Employee>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<Employee>> GetEmployeesAsync(EmployeeWithFilterSpec spec)
        {
            // var spec = new EmployeeWithFilterSpec();
            return await _unitOfWork.Repository<Employee>().ListAsync(spec);
        }
        public async Task<IReadOnlyList<EmployeeNature>> GetEmployeeNatureAsync()
        {
            return await _unitOfWork.Repository<EmployeeNature>().ListAllAsync();
        }
        
        public async Task<Employee> CreateEmployee(Employee emp)
        {
            _unitOfWork.Repository<Employee>().Add(emp);

            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return employee
            return emp;
        }
        // public async Task<AppUser> CreateUser(AppUser user)
        // {
        //     _unitOfWork.Repository<AppUser>().Add(user);
        //     var result = await _unitOfWork.Complete();
        //     if (result <= 0) return null;
        //     // return branch
        //     return user;
        // }
        public async Task<Employee> UpdateEmployee(Employee emp)
        {
            _unitOfWork.Repository<Employee>().Update(emp);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return emp;
        }
        #endregion
        public async Task<EmployeeShiftDetails> CreateEmployeeShift(EmployeeShiftDetails emp)
        {
            _unitOfWork.Repository<EmployeeShiftDetails>().Add(emp);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return employee
            return emp;
        }
        #region EmployeePersonal

        public async Task<EmployeeExperienceInfo> CreateEmployeeExperience(EmployeeExperienceInfo emp)
        {
            _unitOfWork.Repository<EmployeeExperienceInfo>().Add(emp);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return employee
            return emp;
        }
        public async Task<EmployeePersonalInfo> CreateEmployeePersonal(EmployeePersonalInfo emp)
        {
            _unitOfWork.Repository<EmployeePersonalInfo>().Add(emp);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return employee
            return emp;
        }
        public async Task<EmployeePersonalInfo> UpdateEmployeePersonal(EmployeePersonalInfo emp)
        {
            _unitOfWork.Repository<EmployeePersonalInfo>().Update(emp);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return emp;
        }
        public async Task<EmployeePersonalInfo> GetEmployeePersonalById(int Id, string EmpCode = null)
        {
            var param = new EmployeeFindSpec();
            param.Id = Id;
            param.EmpCode = EmpCode;
            var spec = new EmployeePersonalWithFilterSpec(param);
            return await _unitOfWork.Repository<EmployeePersonalInfo>().GetEntityWithSpec(spec);
        }

        public IQueryable<EmployeePersonalInfo> GetEmployeePersonalByIdNoTrack(int Id)
        {
            return _unitOfWork.Repository<EmployeePersonalInfo>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<EmployeePersonalInfo>> GetEmployeesPersonalAsync()
        {
            return await _unitOfWork.Repository<EmployeePersonalInfo>().ListAllAsync();
        }
        #endregion
        #region EmployeeExperiance
        public async Task<EmployeeExperienceInfo> GetEmployeeExperienceById(int Id, string EmpCode = null)
        {
            var param = new EmployeeFindSpec();
            param.Id = Id;
            param.EmpCode = EmpCode;
            var spec = new EmployeeExpWithFilterSpec(param);
            return await _unitOfWork.Repository<EmployeeExperienceInfo>().GetEntityWithSpec(spec);
        }

        public IQueryable<EmployeeExperienceInfo> GetEmployeeExperienceByIdNoTrack(int Id)
        {
            return _unitOfWork.Repository<EmployeeExperienceInfo>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<EmployeeExperienceInfo>> GetEmployeeExperiencesAsync()
        {
            return await _unitOfWork.Repository<EmployeeExperienceInfo>().ListAllAsync();
        }

        public async Task<EmployeeExperienceInfo> UpdateEmployeeExperience(EmployeeExperienceInfo emp)
        {
            _unitOfWork.Repository<EmployeeExperienceInfo>().Update(emp);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return emp;
        }

        public Task<IReadOnlyList<Team>> GetEmployeeTeam(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}