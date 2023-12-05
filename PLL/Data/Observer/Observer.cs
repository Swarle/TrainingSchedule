using Microsoft.AspNetCore.SignalR;
using PLL.Data.Observer.Interfaces;
using PLL.SignalR;

namespace PLL.Data.Observer
{
    public class Observer : IObserver
    {
        private readonly IHubContext<DaoStateNotification> _hub;
        public Observer(IHubContext<DaoStateNotification> hubContext)
        {
            _hub = hubContext;
        }

        public async Task Update(ISubject subject)
        {
            await _hub.Clients.All.SendAsync("Receive", $"{nameof(subject)} have changed his state to {subject._state}");

        }
    }
}
