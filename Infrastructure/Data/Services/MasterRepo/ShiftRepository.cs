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
    public class ShiftRepository : IShiftRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShiftRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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

    }
}