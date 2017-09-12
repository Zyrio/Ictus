using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ictus.Controllers
{
    public class FrameController : Controller
    {
        public FrameController()
        {
        }

        [Route("/frame/comment/{repository}/{fileId}")]
        public IActionResult Comment(string repository, string fileId)
        {
            return View("Comment");
        }
    }
}