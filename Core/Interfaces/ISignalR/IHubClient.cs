using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Notify;

namespace Core.Interfaces.ISignalR
{
    public interface IHubClient
    {
        Task BrodcastMessage(NotifyProps notify);
    }
}