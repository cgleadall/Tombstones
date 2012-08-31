using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tombstones.UI.Web.Areas.Research.Controllers
{
    public class ResourcesController : Controller
    {

        public static string ResourcesResearchLinkPath = "/research/resources/all";
        //
        // GET: /Research/Resources/

        public ActionResult All()
        {
            ViewBag.Title = "Research Resources";
            return View();
        }

        public ActionResult Index(string id)
        {
            var country = id;
            ViewBag.Title = "Research Resouces";
            ViewBag.Country = country;

            var model = new Models.ResourceCollection();
            
            return View(model);
        }

    }
}
