using AspProjectZust.WebUI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspProjectZust.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public UserManager<CustomIdentityUser> _userManager;

        public HomeController(UserManager<CustomIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Birthday()
        {
            return View();
        }

        public IActionResult Events()
        {
            return View();
        }

        public IActionResult Favorite()
        {
            return View();
        }


        public IActionResult Friends()
        {
            return View();
        }

        public IActionResult HelpAndSupport()
        {
            return View();
        }

        public IActionResult LiveChat()
        {
            return View();
        }

        public IActionResult MarketPlace()
        {
            return View();
        }

        public IActionResult Messages()
        {
            return View();
        }


        public IActionResult MyProfile()
        {
            return View();
        }

        public async Task<IActionResult> NewsFeed()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }

        public IActionResult Notifications()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Setting()
        {
            return View();
        }

        public IActionResult Video()
        {
            return View();
        }

        public IActionResult Weather()
        {
            return View();
        }
    }
}

