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
    public class TeamRepository : ITeamRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
        public async Task<Department> GetDepartmentById(int Id)
        {
            var spec = new DepartmentWithDivisionSpec(Id);
            return await _unitOfWork.Repository<Department>().GetEntityWithSpec(spec);
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


            var spec = new TeamWithDepartmentSpec(Team.Id);
            var TeamBeforeUpdate = _unitOfWork.Repository<Team>().GetEntityWithSpecNoTrack(spec).AsEnumerable().SingleOrDefault();
            List<int> previousDetailIds = TeamBeforeUpdate.TeamDetails.Select(x => x.Id).ToList();
            List<int> currentDetailIds = Team.TeamDetails
                .Select(o => o.Id)
                .ToList();

            List<int> deletedDetailIds = previousDetailIds
                .Except(currentDetailIds).ToList();

            foreach (var deletedDetailId in deletedDetailIds)
            {
                var detailSpec = new TeamDetailWithIncludesSpec(deletedDetailId);
                var teamDetailItem = _unitOfWork.Repository<TeamDetails>().GetEntityWithSpecNoTrack(detailSpec).AsEnumerable().SingleOrDefault();

                _unitOfWork.Repository<TeamDetails>().Delete(teamDetailItem); ;
            }

            foreach (var orderDetail in Team.TeamDetails)
            {
                if (orderDetail.Id == 0)
                {
                    _unitOfWork.Repository<TeamDetails>().Add(orderDetail);

                }
                else
                {
                    _unitOfWork.Repository<TeamDetails>().Update(orderDetail);
                }
            }
            _unitOfWork.Repository<Team>().Update(Team);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return Team;
        }


        #endregion
        #region  TeamDetailRepo
        public async Task<TeamDetails> GetTeamDetailById(int Id)
        {
            var spec = new TeamDetailWithIncludesSpec(Id);
            return await _unitOfWork.Repository<TeamDetails>().GetEntityWithSpec(spec);
        }

        public async Task<TeamDetails> CreateTeamDetail(TeamDetails teamDetails)
        {
            _unitOfWork.Repository<TeamDetails>().Add(teamDetails);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return Team
            return teamDetails;
        }

        public async Task<TeamDetails> UpdateTeamDetail(TeamDetails teamDetails)
        {
            _unitOfWork.Repository<TeamDetails>().Update(teamDetails);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return teamDetails;
        }

        public async Task<TeamDetails> DeleteTeamDetail(TeamDetails teamDetails)
        {
            _unitOfWork.Repository<TeamDetails>().Delete(teamDetails);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;
            return teamDetails;
        }
        #endregion
    }
}