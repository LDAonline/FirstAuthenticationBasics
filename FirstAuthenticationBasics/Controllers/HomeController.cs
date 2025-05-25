using Microsoft.AspNetCore.Mvc;

namespace FirstAuthenticationBasics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
