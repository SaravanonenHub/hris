using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Notify;
using Core.Interfaces;
using Core.Interfaces.IActions;

namespace Infrastructure.Data.Services.Notify
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitofwork;

        public NotificationService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<NotifyProps> AddNotification(NotifyProps notify)
        {
            _unitofwork.Repository<NotifyProps>().Add(notify);
            var result = await _unitofwork.Complete();
            return notify;
        }
    }
}