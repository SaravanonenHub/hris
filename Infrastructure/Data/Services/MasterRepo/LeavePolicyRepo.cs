using Core.Entities.Masters;
using Core.Interfaces;
using Core.Interfaces.IMaster;
using Core.Specifications.MasterSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services.MasterRepo
{
    public class LeavePolicyRepo : ILeavePolicyRepo
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeavePolicyRepo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LeavePolicy> Create(LeavePolicy entity)
        {
            _unitOfWork.Repository<LeavePolicy>().Add(entity);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // return Team
            return entity;
        }

        public async Task<IReadOnlyList<LeavePolicy>> GetLeavePoliciesAsync()
        {
            var spec = new LeavePolicySpec();
            return await _unitOfWork.Repository<LeavePolicy>().ListAsync(spec);
        }

        public async Task<LeavePolicy> GetLeavePolicyById(int id)
        {
            var spec = new LeavePolicySpec(id);
            return await _unitOfWork.Repository<LeavePolicy>().GetEntityWithSpec(spec);
        }
        public async Task<LeavePolicy> GetLeavePolicyByIdWithFilter(LeavePolicySpec spec)
        {
            //var spec = new LeavePolicySpec(id);
            return await _unitOfWork.Repository<LeavePolicy>().GetEntityWithSpec(spec);
        }
        public async Task<LeavePolicy> GetbyName(string name)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            var spec = new PolicyWithFilterSpec(param);
            return await _unitOfWork.Repository<LeavePolicy>().GetEntityWithSpec(spec);
        }
        public async Task<LeavePolicy> CheckPolicyonUpdate(string name, int id)
        {
            var param = new MasterUpdateSpecParam();
            param.Search = name;
            param.Id = id;
            var spec = new PolicyWithFilterSpec(param);
            return await _unitOfWork.Repository<LeavePolicy>().GetEntityWithSpec(spec);
        }
        public IQueryable<LeavePolicy> GetLeavePolicybyNoTrack(int Id)
        {
            return _unitOfWork.Repository<LeavePolicy>().GetByIdWithoutTrack(Id);
        }
        public async Task<LeaveType> GetLeaveTypeById(int id)
        {
            return await _unitOfWork.Repository<LeaveType>().GetByIdAsync(id);
        }
        public async Task<LeavePolicy> Update(LeavePolicy entity)
        {
            var spec = new LeavePolicySpec(entity.Id);
            var TeamBeforeUpdate = _unitOfWork.Repository<LeavePolicy>().GetEntityWithSpecNoTrack(spec).AsEnumerable().SingleOrDefault();
            List<int> previousDetailIds = TeamBeforeUpdate.LeavePolicyDetails.Select(x => x.Id).ToList();
            List<int> currentDetailIds = entity.LeavePolicyDetails
                .Select(o => o.Id)
                .ToList();

            List<int> deletedDetailIds = previousDetailIds
                .Except(currentDetailIds).ToList();

            foreach (var deletedDetailId in deletedDetailIds)
            {
                var detailSpec = new LeavePolicyDetailSpec(deletedDetailId);
                var leavePolicyDetail = _unitOfWork.Repository<LeavePolicyDetails>().GetEntityWithSpecNoTrack(detailSpec).AsEnumerable().SingleOrDefault();
                // await _unitOfWork.Repository<TeamDetails>().GetEntityWithSpec(detailSpec);
                _unitOfWork.Repository<LeavePolicyDetails>().Delete(leavePolicyDetail);
            }

            foreach (var orderDetail in entity.LeavePolicyDetails)
            {
                if (orderDetail.Id == 0)
                {
                    _unitOfWork.Repository<LeavePolicyDetails>().Add(orderDetail);

                }
                else
                {
                    _unitOfWork.Repository<LeavePolicyDetails>().Update(orderDetail);
                }
            }
            _unitOfWork.Repository<LeavePolicy>().Update(entity);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            // return order
            return entity;
        }
    }
}
