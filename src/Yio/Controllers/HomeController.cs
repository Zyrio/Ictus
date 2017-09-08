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
            return View ("Index");
        }

        [Route("/{repo}")]
        public IActionResult V2(string repository)
        {
            ViewBag.Repository = repository;

            return View ("Index");
        }

        public string Error()
        {
            return "If you can see this message, something has gone terribly wrong.";
        }
    }
}
