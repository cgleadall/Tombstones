using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tombstones.UI.Web.Controllers
{
    public class ErrorsController : Controller
    {
        //
        // GET: /Errors/

        public ActionResult Http403()
        {
            return View();
        }

        public ActionResult Http404()
        {
            return View();
        }

        public ActionResult general()
        {
            ViewBag.Values = this.RouteData.Values;
            return View();
        }
    }
}
