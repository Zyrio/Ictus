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
            ViewBag.SiteName = Yio.Data.Constants.AppSettingsConstant.SiteName;
            ViewBag.IsOfficial = true;

            if(ViewBag.SiteName != "Yiff.co") {
                ViewBag.IsOfficial = false;
            }

            if(Yio.Data.Constants.VersionConstant.Patch == 0) {
                ViewBag.Version = Yio.Data.Constants.VersionConstant.Release.ToString();
            } else {
                ViewBag.Version = Yio.Data.Constants.VersionConstant.Release.ToString() + "." +
                    Yio.Data.Constants.VersionConstant.Patch.ToString();
            }

            return View ("Index");
        }

        [Route("/{repository}")]
        public IActionResult V2(string repository)
        {
            return View ("Index");
        }

        public string Error()
        {
            return "If you can see this message, something has gone terribly wrong.";
        }
    }
}
