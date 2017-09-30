using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ictus.Data.Constants;
using Ictus.Data.Repositories.Interfaces;

namespace Ictus.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public HomeController(
            IFileRepository fileRepository
        )
        {
            _fileRepository = fileRepository;
        }

        [Route("/frame/comment/{repository}/{fileId}")]
        public IActionResult GetCommentFrame(string repository, string fileId)
        {
            return View("Comment");
        }

        [Route("/")]
        [Route("/{repository}")]
        [Route("/{repository}/{fileId}")]
        public async Task<IActionResult> Index(string repository, string fileId)
        {
            ViewBag.SiteDescription = Ictus.Data.Constants.AppSettingsConstant.SiteDescription;
            ViewBag.SiteIcon = "fa-" + Ictus.Data.Constants.AppSettingsConstant.SiteIcon;
            ViewBag.SiteName = Ictus.Data.Constants.AppSettingsConstant.SiteName;
            ViewBag.IsYiffco = true;

            if(ViewBag.SiteName != "Yiff.co") {
                ViewBag.IsYiffco = false;
            }

            if(Ictus.Data.Constants.VersionConstant.Patch == 0) {
                ViewBag.Version = Ictus.Data.Constants.VersionConstant.Release.ToString();
            } else {
                ViewBag.Version = Ictus.Data.Constants.VersionConstant.Release.ToString() + "." +
                    Ictus.Data.Constants.VersionConstant.Patch.ToString();
            }

            if(Ictus.Data.Constants.VersionConstant.Unstable) {
                ViewBag.Version += "-dev";
            }

            if(!String.IsNullOrEmpty(fileId)) {
                Guid fileGuid = new Guid(fileId);

                Data.Models.File file = await _fileRepository.GetFileById(fileGuid);

                ViewBag.InitialFile = AppSettingsConstant.FileEndpoint + file.Source + "/" + file.Filename;
                ViewBag.InitialFilename = file.Filename;
            }

            return View("Index");
        }

        public string Error()
        {
            return "If you can see this message, something has gone terribly wrong.";
        }
    }
}
