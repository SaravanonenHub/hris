using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Employees;
using Core.Entities.Masters;

namespace Core.Interfaces
{
    public interface IShiftRepository
    {

        Task<Shift> GetShiftById(int Id);
        IQueryable<Shift> GetShiftbyNoTrack(int Id);
        Task<Shift> GetShiftByName(string name);
        Task<Shift> CheckShiftonUpdate(string name, int id);
        Task<IReadOnlyList<Shift>> GetShiftesAsync();
        Task<Shift> CreateShift(Shift Shift);
        Task<Shift> UpdateShift(Shift branch);

    }
}