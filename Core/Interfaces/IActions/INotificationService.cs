using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Notify;

namespace Core.Interfaces.IActions
{
    public interface INotificationService
    {
        Task<NotifyProps> AddNotification(NotifyProps notify);
    }
}