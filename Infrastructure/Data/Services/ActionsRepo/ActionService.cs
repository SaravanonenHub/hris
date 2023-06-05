using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Actions;
using Core.Interfaces;
using Core.Interfaces.IActions;

namespace Infrastructure.Data.Services.ActionsRepo
{
    public class ActionService<T> : IActionService<LeaveAction>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LeaveAction> CreateAction(LeaveAction action)
        {
            _unitOfWork.Repository<LeaveAction>().Add(action);
            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            return action;

        }
    }
}