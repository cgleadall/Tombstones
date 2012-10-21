using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tombstones.UI.Web.Models;

namespace Tombstones.UI.Web.Controllers
{
    public class HomeController : BaseController
    {
        public static string IndexLinkPath = "/";
        public static string LastUpdateLinkPath = "/home/lastupdate/";
        public static string ResearchLinkPath = "/home/research/";
        public static string AboutLinkPath = "/home/about/";
        public static string ContactLinkPath = "/home/contact/";

        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            ViewBag.SubTitle = "We have moved!";
            ViewBag.Message = "News...";

            
            return View();
        }

        public ActionResult LastUpdate()
        {
            var model = new LastUpdateModel();

            return View(model);

        }

        public ActionResult Research()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "We provide genealogical research assistance for the Caribbean.";
            ViewBag.RecentNews = new List<string>{
                "Added links to Research resources",
                "Defined available data for Tombstones research",
                "Create shell for new website on new platform"};
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "If you have any questions...";

            return View();
        }
    }
}
