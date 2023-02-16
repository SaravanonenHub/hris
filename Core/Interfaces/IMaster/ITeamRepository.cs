using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Masters;

namespace Core.Interfaces
{
    public interface ITeamRepository
    {

        #region TeamRepo
        Task<Team> GetTeamById(int Id);
        Task<Department> GetDepartmentById(int Id);
        IQueryable<Team> GetTeambyNoTrack(int Id);
        Task<Team> GetTeamByName(string name);
        Task<Team> CheckTeamonUpdate(string name, int id);
        Task<IReadOnlyList<Team>> GetTeamesAsync();
        Task<Team> CreateTeam(Team team);
        Task<Team> UpdateTeam(Team branch);
        #endregion
        #region TeamDetailRepo
        Task<TeamDetails> GetTeamDetailById(int Id);
        Task<TeamDetails> CreateTeamDetail(TeamDetails team);
        Task<TeamDetails> UpdateTeamDetail(TeamDetails branch);
        Task<TeamDetails> DeleteTeamDetail(TeamDetails branch);
        #endregion
        #region TeamRoleRepo
        Task<TeamRole> GetUserRoleById(int Id);
        IQueryable<TeamRole> GetUserRolebyNoTrack(int Id);
        Task<TeamRole> GetUserRoleByName(string name);
        Task<TeamRole> CheckUserRoleonUpdate(string name, int id);
        Task<IReadOnlyList<TeamRole>> GetUserRoleesAsync();
        Task<TeamRole> CreateUserRole(TeamRole UserRole);
        Task<TeamRole> UpdateUserRole(TeamRole userRole);
        #endregion

    }
}