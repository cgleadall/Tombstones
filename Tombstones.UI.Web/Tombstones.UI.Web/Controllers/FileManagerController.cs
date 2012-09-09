using System;
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
            if (TempData["newFile"] != null)
            {
                ViewBag.NewFile = TempData["newFile"];
            }

            if (TempData["RecordsAdded"] != null && TempData["ImportTimeSpan"] != null)
            {
                ViewBag.RecordsAdded = (int)TempData["RecordsAdded"];
                ViewBag.ImportTimeSpan = (TimeSpan)TempData["ImportTimeSpan"];

                ViewBag.SuccessMessage = string.Format("Imported '{0}' records in {1:0.00} seconds", ViewBag.RecordsAdded, ((TimeSpan)ViewBag.ImportTimeSpan).TotalSeconds);
            }

            var uploadedFiles = RavenSession.Query<Models.UploadedFile>()
                .OrderBy(f => f.FileName);

            var model = ViewModels.FileManagerIndex.Create(Server.MapPath("~/App_Data/uploads/"));
            foreach (var dbFile in uploadedFiles)
            {
                model.UploadedFiles.Add(dbFile);
            }

            return View(model);
        }

        public static string UploadLinkPath = "/filemanager/upload/";
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
                "uploads",
                model.SelectedFileCategory.ToLower());
               
               string savedFileName = Path.Combine(savedFilePath,
               hpf.FileName);

            if( System.IO.File.Exists(savedFileName) )
            {
                ViewBag.ErrorMessage = string.Format("That filename already exists: {0}", 
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
            model.FileBeingUploaded.FullPath = savedFileName;
            model.FileBeingUploaded.FileName = hpf.FileName;

            RavenSession.Store(model.FileBeingUploaded);
            if (TempData.ContainsKey("newFile"))
                TempData["newFile"] = model.FileBeingUploaded;
            else
                TempData.Add("newFile", model.FileBeingUploaded);

            return RedirectToAction("index");
        }

        public static string ImportLinkPath = "/filemanager/import/";
        [HttpGet]
        public ActionResult Import(string id)
        {
            var uploadId = id;
            ViewModels.FileManagerImport model = null;

            Models.UploadedFile uploadFile = RavenSession.Load<Models.UploadedFile>(uploadId);

            if (uploadFile != null && !uploadFile.ImportedAt.HasValue)
            {
                model = ViewModels.FileManagerImport.Create(uploadFile);
                ViewBag.Ready = true;
            }
            else
                RedirectToAction("index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Import(string id, FormCollection formData)
        {
            var uploadedFile = RavenSession.Load<Models.UploadedFile>(id);
            var model = ViewModels.FileManagerImport.Create(uploadedFile);
            
            if (!string.IsNullOrEmpty(formData["BeginImport"]))
            {
                int i=0;
                var stopWatch = System.Diagnostics.Stopwatch.StartNew();
                foreach (var item in model.ReadRowData(uploadedFile.FullPath))
                {
                    var quaker = Models.Quaker.Create(item);

                    if( quaker != null )
                    {
                        RavenSession.Store(quaker);
                    }
                    i++;
                }
                stopWatch.Stop();
                uploadedFile.ImportedAt = DateTime.Now;
                uploadedFile.NumberOfRecords = i;
                
                TempData.Add("RecordsAdded", i);
                TempData.Add("ImportTimeSpan", stopWatch.Elapsed);
            }
            else
            {
                model.ReadHeaders();
                return View(model);
            }
            return RedirectToAction("index");
        }
    }
}
