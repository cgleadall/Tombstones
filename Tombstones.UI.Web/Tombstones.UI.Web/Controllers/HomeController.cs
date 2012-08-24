using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tombstones.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "We have moved...";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "We provide geneological research assistance for the Caribbean.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "If you have any questions...";

            return View();
        }
    }
}
