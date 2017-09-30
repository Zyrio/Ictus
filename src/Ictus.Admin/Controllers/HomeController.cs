using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ictus.Admin.Models;

namespace Ictus.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var isFirstSetup = true;

            if(isFirstSetup) {
                ViewBag.HideNav = true;
                return View("Setup");
            } else {
                return View("Index");
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
