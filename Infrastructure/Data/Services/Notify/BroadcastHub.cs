using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Notify;
using Core.Interfaces.ISignalR;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Data.Services.Notify
{
    public class BroadcastHub : Hub<IHubClient>
    {
        public async Task BrodcastMessage(NotifyProps notification)
        {
            await Clients.All.BrodcastMessage(notification);
        }
    }
}