using Microsoft.AspNetCore.Mvc;

namespace AspProjectZust.WebUI.Controllers
{
    public class MyProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
