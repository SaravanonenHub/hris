using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Masters;
using Core.Interfaces;
using Core.Specifications.MasterSpec;

namespace Infrastructure.Data.Services
{
    public class MasterRepository : IMasterRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public MasterRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region BranchRepo

        public async Task<Branch> CheckBranchonUpdate(string name, int id)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            param.Id = id;
            var spec = new BranchwithFiltersSpec(param);
            return await _unitOfWork.Repository<Branch>().GetEntityWithSpec(spec);
        }

        public async Task<Branch> CreateBranch(Branch branch)
        {
            _unitOfWork.Repository<Branch>().Add(branch);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return branch
            return branch;
        }

        public async Task<Branch> GetBranchById(int Id)
        {
            return await _unitOfWork.Repository<Branch>().GetByIdAsync(Id);
        }

        public async Task<Branch> GetBranchByName(string name)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            var spec = new BranchwithFiltersSpec(param);
            return await _unitOfWork.Repository<Branch>().GetEntityWithSpec(spec);
        }

        public IQueryable<Branch> GetBranchbyNoTrack(int Id)
        {
            return _unitOfWork.Repository<Branch>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<Branch>> GetBranchesAsync()
        {
            return await _unitOfWork.Repository<Branch>().ListAllAsync();
        }

        public async Task<Branch> UpdateBranch(Branch branch)
        {
            _unitOfWork.Repository<Branch>().Update(branch);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return branch;
        }
        #endregion
        #region DepartmentRepo
        public async Task<Department> CheckDepartmentonUpdate(string name, int id)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            param.Id = id;
            var spec = new DepartmentWithFilterSpec(param);
            return await _unitOfWork.Repository<Department>().GetEntityWithSpec(spec);
        }

        public async Task<Department> CreateDepartment(Department Department)
        {
            _unitOfWork.Repository<Department>().Add(Department);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return Department
            return Department;
        }

        public async Task<Department> GetDepartmentById(int Id)
        {
            var spec = new DepartmentWithDivisionSpec(Id);
            return await _unitOfWork.Repository<Department>().GetEntityWithSpec(spec);
        }

        public async Task<Department> GetDepartmentByName(string name)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            var spec = new DepartmentWithFilterSpec(param);
            return await _unitOfWork.Repository<Department>().GetEntityWithSpec(spec);
        }

        public IQueryable<Department> GetDepartmentbyNoTrack(int Id)
        {
            return _unitOfWork.Repository<Department>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<Department>> GetDepartmentesAsync()
        {
            var spec = new DepartmentWithDivisionSpec();
            return await _unitOfWork.Repository<Department>().ListAsync(spec);
        }

        public async Task<Department> UpdateDepartment(Department Department)
        {
            _unitOfWork.Repository<Department>().Update(Department);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return Department;
        }
        #endregion
        #region DesignationRepo
        public async Task<Designation> CheckDesignationonUpdate(string name, int id)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            param.Id = id;
            var spec = new DesignationWithFilterSpec(param);
            return await _unitOfWork.Repository<Designation>().GetEntityWithSpec(spec);
        }

        public async Task<Designation> CreateDesignation(Designation Designation)
        {
            _unitOfWork.Repository<Designation>().Add(Designation);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return Designation
            return Designation;
        }

        public async Task<Designation> GetDesignationById(int Id)
        {
            return await _unitOfWork.Repository<Designation>().GetByIdAsync(Id);
        }

        public async Task<Designation> GetDesignationByName(string name)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            var spec = new DesignationWithFilterSpec(param);
            return await _unitOfWork.Repository<Designation>().GetEntityWithSpec(spec);
        }

        public IQueryable<Designation> GetDesignationbyNoTrack(int Id)
        {
            return _unitOfWork.Repository<Designation>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<Designation>> GetDesignationesAsync()
        {
            return await _unitOfWork.Repository<Designation>().ListAllAsync();
        }

        public async Task<Designation> UpdateDesignation(Designation Designation)
        {
            _unitOfWork.Repository<Designation>().Update(Designation);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return Designation;
        }
        #endregion

        #region DivisionRepo
        public async Task<Division> CheckDivisiononUpdate(string name, int id)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            param.Id = id;
            var spec = new DivisionWithFilterSpec(param);
            return await _unitOfWork.Repository<Division>().GetEntityWithSpec(spec);
        }

        public async Task<Division> CreateDivision(Division Division)
        {
            _unitOfWork.Repository<Division>().Add(Division);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return Division
            return Division;
        }

        public async Task<Division> GetDivisionById(int Id)
        {
            return await _unitOfWork.Repository<Division>().GetByIdAsync(Id);
        }

        public async Task<Division> GetDivisionByName(string name)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            var spec = new DivisionWithFilterSpec(param);
            return await _unitOfWork.Repository<Division>().GetEntityWithSpec(spec);
        }

        public IQueryable<Division> GetDivisionbyNoTrack(int Id)
        {
            return _unitOfWork.Repository<Division>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<Division>> GetDivisionesAsync()
        {
            return await _unitOfWork.Repository<Division>().ListAllAsync();
        }

        public async Task<Division> UpdateDivision(Division Division)
        {
            _unitOfWork.Repository<Division>().Update(Division);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return Division;
        }
        #endregion

        #region ShiftRepo
        public async Task<Shift> CheckShiftonUpdate(string name, int id)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            param.Id = id;
            var spec = new ShiftWithFilterSpec(param);
            return await _unitOfWork.Repository<Shift>().GetEntityWithSpec(spec);
        }

        public async Task<Shift> CreateShift(Shift Shift)
        {
            _unitOfWork.Repository<Shift>().Add(Shift);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return Shift
            return Shift;
        }

        public async Task<Shift> GetShiftById(int Id)
        {
            return await _unitOfWork.Repository<Shift>().GetByIdAsync(Id);
        }

        public async Task<Shift> GetShiftByName(string name)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            var spec = new ShiftWithFilterSpec(param);
            return await _unitOfWork.Repository<Shift>().GetEntityWithSpec(spec);
        }

        public IQueryable<Shift> GetShiftbyNoTrack(int Id)
        {
            return _unitOfWork.Repository<Shift>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<Shift>> GetShiftesAsync()
        {
            return await _unitOfWork.Repository<Shift>().ListAllAsync();
        }

        public async Task<Shift> UpdateShift(Shift Shift)
        {
            _unitOfWork.Repository<Shift>().Update(Shift);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return Shift;
        }
        #endregion
        #region TeamRoleRepo
        public async Task<TeamRole> CheckUserRoleonUpdate(string name, int id)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            param.Id = id;
            var spec = new UserRoleWithFilterSpec(param);
            return await _unitOfWork.Repository<TeamRole>().GetEntityWithSpec(spec);
        }

        public async Task<TeamRole> CreateUserRole(TeamRole UserRole)
        {
            _unitOfWork.Repository<TeamRole>().Add(UserRole);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return UserRole
            return UserRole;
        }

        public async Task<TeamRole> GetUserRoleById(int Id)
        {
            return await _unitOfWork.Repository<TeamRole>().GetByIdAsync(Id);
        }

        public async Task<TeamRole> GetUserRoleByName(string name)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            var spec = new UserRoleWithFilterSpec(param);
            return await _unitOfWork.Repository<TeamRole>().GetEntityWithSpec(spec);
        }

        public IQueryable<TeamRole> GetUserRolebyNoTrack(int Id)
        {
            return _unitOfWork.Repository<TeamRole>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<TeamRole>> GetUserRoleesAsync()
        {
            return await _unitOfWork.Repository<TeamRole>().ListAllAsync();
        }

        public async Task<TeamRole> UpdateUserRole(TeamRole UserRole)
        {
            _unitOfWork.Repository<TeamRole>().Update(UserRole);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return UserRole;
        }
        #endregion
        #region  UserLevelRepo
        public async Task<UserLevel> CheckUserLevelonUpdate(string name, int id)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            param.Id = id;
            var spec = new UserLevelWithFilterSpec(param);
            return await _unitOfWork.Repository<UserLevel>().GetEntityWithSpec(spec);
        }

        public async Task<UserLevel> CreateUserLevel(UserLevel UserLevel)
        {
            _unitOfWork.Repository<UserLevel>().Add(UserLevel);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return UserLevel
            return UserLevel;
        }

        public async Task<UserLevel> GetUserLevelById(int Id)
        {
            return await _unitOfWork.Repository<UserLevel>().GetByIdAsync(Id);
        }

        public async Task<UserLevel> GetUserLevelByName(string name)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            var spec = new UserLevelWithFilterSpec(param);
            return await _unitOfWork.Repository<UserLevel>().GetEntityWithSpec(spec);
        }

        public IQueryable<UserLevel> GetUserLevelbyNoTrack(int Id)
        {
            return _unitOfWork.Repository<UserLevel>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<UserLevel>> GetUserLevelesAsync()
        {
            return await _unitOfWork.Repository<UserLevel>().ListAllAsync();
        }

        public async Task<UserLevel> UpdateUserLevel(UserLevel UserLevel)
        {
            _unitOfWork.Repository<UserLevel>().Update(UserLevel);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return UserLevel;
        }
        #endregion
        #region TeamRepo
        public async Task<Team> CheckTeamonUpdate(string name, int id)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            param.Id = id;
            var spec = new TeamWithFilterSpec(param);
            return await _unitOfWork.Repository<Team>().GetEntityWithSpec(spec);
        }

        public async Task<Team> CreateTeam(Team Team)
        {
            _unitOfWork.Repository<Team>().Add(Team);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return Team
            return Team;
        }

        public async Task<Team> GetTeamById(int Id)
        {
            var spec = new TeamWithDepartmentSpec(Id);
            return await _unitOfWork.Repository<Team>().GetEntityWithSpec(spec);
        }

        public async Task<Team> GetTeamByName(string name)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            var spec = new TeamWithFilterSpec(param);
            return await _unitOfWork.Repository<Team>().GetEntityWithSpec(spec);
        }

        public IQueryable<Team> GetTeambyNoTrack(int Id)
        {
            return _unitOfWork.Repository<Team>().GetByIdWithoutTrack(Id);
        }

        public async Task<IReadOnlyList<Team>> GetTeamesAsync()
        {
            var spec = new TeamWithDepartmentSpec();
            return await _unitOfWork.Repository<Team>().ListAsync(spec);
        }

        public async Task<Team> UpdateTeam(Team Team)
        {
            _unitOfWork.Repository<Team>().Update(Team);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return Team;
        }
        #endregion
    }
}