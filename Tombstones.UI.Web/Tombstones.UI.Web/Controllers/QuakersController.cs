using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tombstones.UI.Web.Models;
using Tombstones.UI.Web.ViewModels;

namespace Tombstones.UI.Web.Controllers
{
    public class QuakersController : BaseController
    {
        //
        // GET: /Quakers/
        public static string IndexLinkPath = "/quakers/";
        public ActionResult Index()
        {
            var model = new ViewModels.QuakersIndex();

            model.NumberOfRecords = RavenSession.Query<Models.Quaker>().Count();

            return View();
        }

        // POST: /Quakers/
        [HttpPost]
        public ActionResult Index(FormCollection formData)
        {
            string surname;
            string firstname;
            IQueryable<Models.Quaker> query;
            query = RavenSession.Query<Models.Quaker>();

            if( formData["Surname"] != null && !string.IsNullOrEmpty(formData["Surname"]) )
            {
                var startsWith = !string.IsNullOrEmpty(formData["surnameStartsWith"]);
                surname = formData["Surname"];
                if (startsWith)
                {
                    query = query.Where(q => q.Surname.StartsWith(surname));
                }
                else
                {
                    query = query.Where(q => q.Surname == surname);
                }
            }
            if( formData["Firstname"] != null && !string.IsNullOrEmpty(formData["Firstname"]) )
            {    
                firstname = formData["Firstname"];
                query = query.Where( q => q.FirstName == firstname);
            }

            var data = query.ToList();
            ViewBag.Data = data;

            return View();
        }

        //
        // GET: /Quakers/Details/5
        public static string DetailsLinkPath = "/quakers/details/";
        public static string DetailsLinkPathFormat = "/quakers/details/{0}";
        public ActionResult Details(string id)
        {
            Quaker model;
            if (!string.IsNullOrEmpty(id))
            {
                model = RavenSession.Load<Quaker>(id);
            }
            else
            {
                return RedirectToAction("index");
            }
            return View(model);
        }

    }
}
