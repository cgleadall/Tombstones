using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tombstones.UI.Web.Controllers
{
    public class MinistersController : BaseController
    {
        //
        // GET: /Ministers/

        public static string IndexLinkPath = "/ministers/";
        public ActionResult Index()
        {
            var model = new ViewModels.MinistersIndex();

            if (Session["searchModel"] == null)
            {
                model.NumberOfRecords = RavenSession.Query<Models.Minister>().Count();
            }
            else
            {
                model = (ViewModels.MinistersIndex)Session["searchModel"];
            }
            return View(model);
        }

        // POST: /ministers/
        [HttpPost]
        public ActionResult Index(ViewModels.MinistersIndex model)
        {
            ViewBag.ErrorMessage = string.Empty;

            IQueryable<Models.Minister> query;
            query = RavenSession.Query<Models.Minister>();

            if (model != null && !string.IsNullOrEmpty(model.Lastname))
            {
                if (model.LastnameStartsWith)
                {
                    if (model.Lastname.Length >= 3)
                        query = query.Where(q => q.LastName.StartsWith(model.Lastname));
                    else
                        ViewBag.ErrorMessage = "You must have at least 3 letters to do a 'starts with' Lastname search";
                }
                else
                {
                    query = query.Where(q => q.LastName == model.Lastname);
                }
            }
            if (model != null && !string.IsNullOrEmpty(model.Firstname))
            {
                if (model.FirstnameStartsWith)
                {
                    if (model.Firstname.Length >= 2)
                        query = query.Where(q => q.FirstName.StartsWith(model.Firstname) || q.OtherNames.StartsWith(model.Firstname));
                    else
                        ViewBag.ErrorMessage += "You must have at least 2 letters to do a 'starts with' Firstname search";
                }
                else
                {
                    query = query.Where(q => q.FirstName == model.Firstname || q.OtherNames.Contains(model.Firstname));
                }
            }

            if (string.IsNullOrEmpty(ViewBag.ErrorMessage))
            {
                model.SearchResults = query.ToList();
                Session.Add("searchModel", model);
            }

            if (model != null && model.SearchResults != null && model.SearchResults.Count == 0)
                ViewBag.ErrorMessage = "Your search returned no results.";
            return View(model);
        }

        // GET: /Quakers/Details/5
        public static string DetailsLinkPath = "/ministers/details/";
        public static string DetailsLinkPathFormat = "/ministers/details/{0}";
        public ActionResult Details(string id)
        {
            Models.Minister model;
            if (!string.IsNullOrEmpty(id))
            {
                model = RavenSession.Load<Models.Minister>(id);
            }
            else
            {
                return RedirectToAction("index");
            }
            return View(model);
        }

    }
}
