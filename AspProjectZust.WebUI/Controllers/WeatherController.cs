using Microsoft.AspNetCore.Mvc;

namespace AspProjectZust.WebUI.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
