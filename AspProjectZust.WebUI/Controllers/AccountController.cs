using AspProjectZust.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspProjectZust.WebUI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var obj = new RegisterViewModel();
            return View(obj);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            
            return View();
        }

    }
}
