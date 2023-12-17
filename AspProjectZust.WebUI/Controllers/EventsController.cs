using Microsoft.AspNetCore.Mvc;

namespace AspProjectZust.WebUI.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
