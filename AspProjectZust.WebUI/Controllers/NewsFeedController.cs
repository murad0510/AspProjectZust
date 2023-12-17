using Microsoft.AspNetCore.Mvc;

namespace AspProjectZust.WebUI.Controllers
{
    public class NewsFeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
