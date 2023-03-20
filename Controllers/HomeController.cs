using Microsoft.AspNetCore.Mvc;

namespace JewelleryStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
