using Microsoft.AspNetCore.Mvc;

namespace JewelleryStore
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
