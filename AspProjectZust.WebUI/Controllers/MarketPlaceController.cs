using Microsoft.AspNetCore.Mvc;

namespace AspProjectZust.WebUI.Controllers
{
    public class MarketPlaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
