using Microsoft.AspNetCore.Mvc;

namespace Yio.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [Route("/")]
        [Route("/{repository}")]
        [Route("/{repository}/{fileId}")]
        public IActionResult Index(string repository, string fileId)
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

            return View("Index");
        }

        [Route("/frame/comment/{repository}/{fileId}")]
        public IActionResult Comment(string repository, string fileId)
        {
            return View("Comment");
        }

        public string Error()
        {
            return "If you can see this message, something has gone terribly wrong.";
        }
    }
}
