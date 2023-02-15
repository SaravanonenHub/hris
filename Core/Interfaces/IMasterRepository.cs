using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Masters;

namespace Core.Interfaces
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
        #region ShiftRepo
        Task<Shift> GetShiftById(int Id);
        IQueryable<Shift> GetShiftbyNoTrack(int Id);
        Task<Shift> GetShiftByName(string name);
        Task<Shift> CheckShiftonUpdate(string name, int id);
        Task<IReadOnlyList<Shift>> GetShiftesAsync();
        Task<Shift> CreateShift(Shift Shift);
        Task<Shift> UpdateShift(Shift branch);
        #endregion
        #region UserRoleRepo
        Task<TeamRole> GetUserRoleById(int Id);
        IQueryable<TeamRole> GetUserRolebyNoTrack(int Id);
        Task<TeamRole> GetUserRoleByName(string name);
        Task<TeamRole> CheckUserRoleonUpdate(string name, int id);
        Task<IReadOnlyList<TeamRole>> GetUserRoleesAsync();
        Task<TeamRole> CreateUserRole(TeamRole UserRole);
        Task<TeamRole> UpdateUserRole(TeamRole userRole);
        #endregion
        #region UserLevelRepo
        Task<UserLevel> GetUserLevelById(int Id);
        IQueryable<UserLevel> GetUserLevelbyNoTrack(int Id);
        Task<UserLevel> GetUserLevelByName(string name);
        Task<UserLevel> CheckUserLevelonUpdate(string name, int id);
        Task<IReadOnlyList<UserLevel>> GetUserLevelesAsync();
        Task<UserLevel> CreateUserLevel(UserLevel UserLevel);
        Task<UserLevel> UpdateUserLevel(UserLevel branch);
        #endregion
        #region TeamRepo
        Task<Team> GetTeamById(int Id);
        IQueryable<Team> GetTeambyNoTrack(int Id);
        Task<Team> GetTeamByName(string name);
        Task<Team> CheckTeamonUpdate(string name, int id);
        Task<IReadOnlyList<Team>> GetTeamesAsync();
        Task<Team> CreateTeam(Team team);
        Task<Team> UpdateTeam(Team branch);
        #endregion
    }
}