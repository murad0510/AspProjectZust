using Microsoft.AspNetCore.Mvc;

namespace AspProjectZust.WebUI.Controllers
{
    public class LiveChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
