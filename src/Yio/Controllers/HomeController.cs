using Microsoft.AspNetCore.Mvc;

namespace Yio.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Yiff.co";
            ViewBag.ShowNav = "True";
            ViewBag.Color = "#33FFB8";

            return View();
        }

        public string Error()
        {
            return "If you can see this message, something has gone terribly wrong.";
        }
    }
}
