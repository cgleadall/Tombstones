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
            
            if (Session["searchModel"] == null)
            {
                model.NumberOfRecords = RavenSession.Query<Models.Quaker>().Count();
            }
            else
            {
                model = (ViewModels.QuakersIndex)Session["searchModel"];
            }
            return View(model);
        }

        // POST: /Quakers/
        [HttpPost]
        public ActionResult Index(ViewModels.QuakersIndex model)
        {
            ViewBag.ErrorMessage = string.Empty;

            IQueryable<Models.Quaker> query;
            query = RavenSession.Query<Models.Quaker>();

            if (model != null && !string.IsNullOrEmpty(model.Surname))
            {
                if (model.SurnameStartsWith)
                {
                    if (model.Surname.Length >= 3)
                        query = query.Where(q => q.Surname.StartsWith(model.Surname));
                    else
                        ViewBag.ErrorMessage = "You must have at least 3 letters to do a 'starts with' Surname search";
                }
                else
                {
                    query = query.Where(q => q.Surname == model.Surname);
                }
            }
            if (model != null && !string.IsNullOrEmpty(model.Firstname))
            {
                if (model.FirstnameStartsWith)
                {
                    if (model.Firstname.Length >= 2)
                        query = query.Where(q => q.FirstName.StartsWith(model.Firstname));
                    else
                        ViewBag.ErrorMessage += "You must have at least 2 letters to do a 'starts with' Firstname search";
                }
                else
                {
                    query = query.Where(q => q.FirstName == model.Firstname);
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
