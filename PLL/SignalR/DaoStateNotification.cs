using Microsoft.AspNetCore.SignalR;
using PLL.Data.Dao.SqlDao;
using PLL.Data.Entity;
using PLL.Data.Observer.Interfaces;

namespace PLL.SignalR
{
    public class DaoStateNotification : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }

    }
}
