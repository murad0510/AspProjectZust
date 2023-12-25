using AspProjectZust.Entities.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AspProjectZust.WebUI.Hubs
{
    public class UserHub : Hub
    {
        private UserManager<CustomIdentityUser> _userManager;
        private IHttpContextAccessor _contextAccessor;
        private CustomIdentityDbContext _customIdentityDbContext;

        public UserHub(UserManager<CustomIdentityUser> userManager, IHttpContextAccessor contextAccessor, CustomIdentityDbContext customIdentityDbContext)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _customIdentityDbContext = customIdentityDbContext;
        }

        public async override Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            var userItem = _customIdentityDbContext.Users.SingleOrDefault(s => s.Id == user.Id);
            userItem.IsOnline = true;
            await _customIdentityDbContext.SaveChangesAsync();

            var info = $"Connected User : {userItem.UserName}";
            await Clients.Others.SendAsync("Connect", info);
            //return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
