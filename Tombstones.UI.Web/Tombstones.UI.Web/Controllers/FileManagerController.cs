﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Tombstones.UI.Web.Controllers
{
    public class FileManagerController : BaseController
    {
        //
        // GET: /FileManager/

        public static string IndexLinkPath = "/filemanager/";

        public ActionResult Index()
        {
            return View();
        }

        public static string UploadLinkPath = "/filemanager/upload";
        [HttpGet]
        public ActionResult Upload()
        {
            var model = new ViewModels.FileUpload();

            return View(model);
        }

        [HttpPost]
        public ActionResult Upload(ViewModels.FileUpload model, FormCollection formdata)
        {
            model.FileBeingUploaded = new Models.UploadedFile {
                Category = model.SelectedFileCategory,
                UploadedAt = DateTime.Now};

            HttpPostedFileBase hpf = Request.Files["uploadedFile"] as HttpPostedFileBase;
            if ( hpf == null || hpf.ContentLength == 0)
            {
                ViewBag.ErrorMessage = "That file contained no data...";
                return ViewBag(model);
            }

            string savedFilePath = Path.Combine(
               AppDomain.CurrentDomain.BaseDirectory,"App_Data", 
                "uploads");
               
               string savedFileName = Path.Combine(savedFilePath,
               hpf.FileName);

            if( System.IO.File.Exists(savedFileName) )
            {
                ViewBag.InfoMessage = string.Format("That filename already exists: {0}", 
                    savedFileName.Substring(savedFileName.IndexOf("App_Data") + "App_Data".Length+1));
                return View(model);
            }
            try
            {
                hpf.SaveAs(savedFileName);

            }
            catch (Exception )
            {
                System.IO.Directory.CreateDirectory(savedFilePath);
                hpf.SaveAs(savedFileName);
            }
            model.FileBeingUploaded.FileName = savedFileName;

            RavenSession.Store(model.FileBeingUploaded);

            return View(model);
        }
    }
}