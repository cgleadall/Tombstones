using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tombstones.UI.Web.Areas.Tombstones.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Tombstones/Home/

        public ActionResult Index()
        {
            var model = new ViewModels.HomeIndex
            {
                Title = "Tombstones Home",
                LastUpdateDate = DateTime.Now
            };
            return View(model);
        }

    }
}
