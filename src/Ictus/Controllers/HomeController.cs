using System.Threading.Tasks;
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
            ViewBag.IsYiffco = true;

            if(ViewBag.SiteName != "Yiff.co") {
                ViewBag.IsYiffco = false;
            }

            if(Yio.Data.Constants.VersionConstant.Patch == 0) {
                ViewBag.Version = Yio.Data.Constants.VersionConstant.Release.ToString();
            } else {
                ViewBag.Version = Yio.Data.Constants.VersionConstant.Release.ToString() + "." +
                    Yio.Data.Constants.VersionConstant.Patch.ToString();
            }

            return View("Index");
        }

        //[Route("/{repository}/{fileId}.{extension}")]
        //public async Task<IActionResult> Raw(string repository, string fileId, string extension)
        //{
        //    var file = System.IO.File.OpenRead("/home/ducky/Pictures/607.jpg");
        //    string type = "application/octet-stream";
        //
        //    if(extension == "jpg" || extension == "jpeg")
        //    {
        //       type =  "image/jpeg";
        //    } else if(extension == "png")
        //    {
        //        type = "image/jpeg";
        //    } else if(extension == "gif")
        //    {
        //        type = "image/gif";
        //    }
        //
        //    return File(file, type);
        //}

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
