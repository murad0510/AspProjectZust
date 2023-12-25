using AspProjectZust.Entities.Entity;
using AspProjectZust.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace AspProjectZust.WebUI.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        private UserManager<CustomIdentityUser> _userManager;

        public UserInfoViewComponent(UserManager<CustomIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public ViewViewComponentResult Invoke()
        {
            var user =  _userManager.GetUserAsync(HttpContext.User).Result;

            var user2 = new UserInfoViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
            };

            return View(user2);
        }
    }
}
