using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AspProjectZust.WebUI.Hubs
{
    public class UserHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            Clients.Others.SendAsync("Slam", "d");
            //return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
