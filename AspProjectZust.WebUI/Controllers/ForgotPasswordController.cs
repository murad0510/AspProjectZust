using Microsoft.AspNetCore.Mvc;

namespace AspProjectZust.WebUI.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
