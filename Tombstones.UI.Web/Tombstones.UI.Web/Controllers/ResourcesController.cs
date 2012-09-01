using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tombstones.UI.Web.Controllers
{
    public class ResourcesController : BaseController
    {
        //
        // GET: /Resources/
        public static string ResourcesResearchLinkPath = "/resources/";

        public ActionResult Index(string id)
        {
            string country = id;
            IList<Models.Resource> model;

            if (string.IsNullOrEmpty(country))
            {
                ViewBag.Title = "All";
                model = RavenSession.Query<Models.Resource>().OrderBy(r => r.Country).ThenBy(r => r.Title)
                    .ToList<Models.Resource>();
            }
            else
            {
                ViewBag.Title = country;
                model = RavenSession.Query<Models.Resource>().Where(r => r.Country.ToLower().CompareTo(country.ToLower()) == 0)
                    .ToList<Models.Resource>();
            }
            model = model.OrderBy(m => m.Country).ThenBy(m => m.Title).ToList();

            return View(model);

        }

        public ActionResult Admin(string id)
        {
            string country = id;
            IList<Models.Resource> model;

            if (string.IsNullOrEmpty(country))
            {
                ViewBag.Title = "All";
                model = RavenSession.Query<Models.Resource>().OrderBy(r => r.Country).ThenBy(r => r.Title)
                    .ToList<Models.Resource>();
            }
            else
            {
                ViewBag.Title = country;
                model = RavenSession.Query<Models.Resource>().Where(r => r.Country.ToLower().CompareTo(country.ToLower()) == 0)
                    .ToList<Models.Resource>();
            }
            model = model.OrderBy(m => m.Country).ThenBy(m => m.Title).ToList();

            if (TempData["NewObject"] != null)
            {
                var newObject = ((Models.Resource)TempData["NewObject"]);
                if ( !model.Any(m => m.Id == newObject.Id) )
                {
                    model.Add((Models.Resource)TempData["NewObject"]);
                    model = model.OrderBy(m => m.Country).ThenBy(m => m.Title).ToList();
                }
            }


            return View(model);
        }

        //
        // GET: /Resources/Details/5

        public ActionResult Details(string id)
        {
            var model = RavenSession.Load<Models.Resource>(id);
            return View(model);
        }

        //
        // GET: /Resources/Create

        public ActionResult Create()
        {
            var model = new Models.Resource();

            return View(model);
        }

        //
        // POST: /Resources/Create

        [HttpPost]
        public ActionResult Create(Models.Resource model)
        {
            try
            {
                RavenSession.Store(model);
                TempData.Add("NewObject", model);

                return RedirectToAction("Admin");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult AddAddress(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("admin");

            Models.Resource model = RavenSession.Load<Models.Resource>(id.ToString());

            return View(model);
        }

        [HttpPost]
        public ActionResult AddAddress(Models.Resource model, string newAddress)
        {
            try
            {
                var researchResource = RavenSession.Load<Models.Resource>(model.Id);

                researchResource.Addresses.Add(newAddress);

                RavenSession.Store(researchResource, researchResource.Id);

                return RedirectToAction("edit", new { Id = researchResource.Id });
            }
            catch 
            {

                return View(model);
            }
        }

        //
        // GET: /Resources/Edit/5

        public ActionResult Edit(string id)
        {
            var model = RavenSession.Load<Models.Resource>(id);
            return View(model);
        }

        //
        // POST: /Resources/Edit/5

        [HttpPost]
        public ActionResult Edit(Models.Resource model)
        {
            try
            {
                RavenSession.Store(model, model.Id);

                return RedirectToAction("admin");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Resources/Delete/5

        public ActionResult Delete(string id)
        {
            var resource = RavenSession.Load<Models.Resource>(id);
            return View(resource);
        }

        //
        // POST: /Resources/Delete/5

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                var resource = RavenSession.Load<Models.Resource>(id);
                RavenSession.Delete<Models.Resource>(resource);

                return RedirectToAction("admin");
            }
            catch
            {
                return View();
            }
        }
    }
}
