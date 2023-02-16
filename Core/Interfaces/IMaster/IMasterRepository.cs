using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Masters;

namespace Core.Interfaces.IMaster
{
    public interface IMasterRepository
    {
        #region BranchRepo
        Task<Branch> GetBranchById(int Id);
        IQueryable<Branch> GetBranchbyNoTrack(int Id);
        Task<Branch> GetBranchByName(string name);
        Task<Branch> CheckBranchonUpdate(string name, int id);
        Task<IReadOnlyList<Branch>> GetBranchesAsync();
        Task<Branch> CreateBranch(Branch branch);
        Task<Branch> UpdateBranch(Branch branch);
        #endregion
        #region DepartmentRepo
        Task<Department> GetDepartmentById(int Id);
        IQueryable<Department> GetDepartmentbyNoTrack(int Id);
        Task<Department> GetDepartmentByName(string name);
        Task<Department> CheckDepartmentonUpdate(string name, int id);
        Task<IReadOnlyList<Department>> GetDepartmentesAsync();
        Task<Department> CreateDepartment(Department Department);
        Task<Department> UpdateDepartment(Department branch);
        #endregion
        #region DesignationRepo
        Task<Designation> GetDesignationById(int Id);
        IQueryable<Designation> GetDesignationbyNoTrack(int Id);
        Task<Designation> GetDesignationByName(string name);
        Task<Designation> CheckDesignationonUpdate(string name, int id);
        Task<IReadOnlyList<Designation>> GetDesignationesAsync();
        Task<Designation> CreateDesignation(Designation Designation);
        Task<Designation> UpdateDesignation(Designation branch);
        #endregion
        #region DivisionRepo
        Task<Division> GetDivisionById(int Id);
        IQueryable<Division> GetDivisionbyNoTrack(int Id);
        Task<Division> GetDivisionByName(string name);
        Task<Division> CheckDivisiononUpdate(string name, int id);
        Task<IReadOnlyList<Division>> GetDivisionesAsync();
        Task<Division> CreateDivision(Division Division);
        Task<Division> UpdateDivision(Division branch);
        #endregion

    }
}