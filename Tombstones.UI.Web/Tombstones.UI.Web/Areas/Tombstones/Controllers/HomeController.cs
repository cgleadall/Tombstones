using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tombstones.UI.Web.Areas.Tombstones.Controllers
{
    public class HomeController : Controller
    {
        public static string IndexLinkPath = "/tombstones/";
        //
        // GET: /Tombstones/Home/

        public ActionResult Index()
        {
            var model = new ViewModels.HomeIndex
            {
                Title = "Tombstones Research",
                LastUpdateDate = DateTime.Now
            };
            return View(model);
        }

    }
}
