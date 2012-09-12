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
#if DEBUG
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
#else
            return RedirectToRoute("Default");
#endif
        }

        public static string UploadLinkPath = "/filemanager/upload/";
        [HttpGet]
        public ActionResult Upload()
        {
#if DEBUG
            var model = new ViewModels.FileUpload();

            return View(model);
#else
            return RedirectToRoute("Default");
#endif
        }

        [HttpPost]
        public ActionResult Upload(ViewModels.FileUpload model, FormCollection formdata)
        {
#if DEBUG
            model.FileBeingUploaded = new Models.UploadedFile
            {
                Category = model.SelectedFileCategory,
                UploadedAt = DateTime.Now
            };

            HttpPostedFileBase hpf = Request.Files["uploadedFile"] as HttpPostedFileBase;
            if (hpf == null || hpf.ContentLength == 0)
            {
                ViewBag.ErrorMessage = "That file contained no data...";
                return ViewBag(model);
            }

            string savedFilePath = Path.Combine(
               AppDomain.CurrentDomain.BaseDirectory, "App_Data",
                "uploads",
                model.SelectedFileCategory.ToLower());

            string savedFileName = Path.Combine(savedFilePath,
            hpf.FileName);

            if (System.IO.File.Exists(savedFileName))
            {
                ViewBag.ErrorMessage = string.Format("That filename already exists: {0}",
                    savedFileName.Substring(savedFileName.IndexOf("App_Data") + "App_Data".Length + 1));
                return View(model);
            }
            try
            {
                hpf.SaveAs(savedFileName);

            }
            catch (Exception)
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
#else
            return RedirectToRoute("Default");
#endif
        }

        public static string ImportLinkPath = "/filemanager/import/";
        [HttpGet]
        public ActionResult Import(string id)
        {
#if DEBUG
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
#else
            return RedirectToRoute("Default");
#endif
        }

        [HttpPost]
        public ActionResult Import(string id, FormCollection formData)
        {
#if DEBUG
            var uploadedFile = RavenSession.Load<Models.UploadedFile>(id);
            var model = ViewModels.FileManagerImport.Create(uploadedFile);

            if (!string.IsNullOrEmpty(formData["BeginImport"]))
            {
                int i = 0;
                var stopWatch = System.Diagnostics.Stopwatch.StartNew();
                foreach (var item in model.ReadRowData(uploadedFile.FullPath))
                {
                    switch (model.Category.ToLower())
                    {
                        case "quakers":
                            var quaker = Models.Quaker.Create(item, uploadedFile.Id);
                            if (quaker != null)
                            {
                                RavenSession.Store(quaker);
                                i++;
                            }
                            break;
                        case "ministers":
                            var minister = Models.Minister.Create(item, uploadedFile.Id);
                            if (minister != null)
                            {
                                RavenSession.Store(minister);
                                i++;
                            }
                            break;
                        default:
                            break;
                    }
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
#else
            return RedirectToRoute("Default");
#endif
        }


        public static string UndoImportLinkPath = "/filemanager/undoimport/";
        [HttpGet]
        public ActionResult UndoImport(string id)
        {
            ViewModels.FileManagerUndoImport model;
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("index");

            var uploadedFile = RavenSession.Load<Models.UploadedFile>(id);
            model = ViewModels.FileManagerUndoImport.Create(uploadedFile.Id);
            model.RemainingRecords = RavenSession.Query<Models.Quaker>().Count();

            return View(model);
        }

        [HttpPost]
        public ActionResult UndoImport(ViewModels.FileManagerUndoImport model)
        {
            if (!model.ConfirmRemoval)
            {
                return View(model);
            }

            var stopWatch = System.Diagnostics.Stopwatch.StartNew();

            var recordsToDelete = RavenSession.Query<Models.Quaker>().Where(q => q.UploadedFileId == model.UploadedFileId)
                .Take(model.BatchSize);

            int numberOfRecordsToDelete = recordsToDelete.Count();
            if (numberOfRecordsToDelete == 0)
            {
                var uploadedFile = RavenSession.Load<Models.UploadedFile>(model.UploadedFileId);
                uploadedFile.ImportedAt = null;
                uploadedFile.NumberOfRecords = null;

                model.RemainingRecords = 0;
                model.ConfirmRemoval = false;
                return RedirectToAction("index");
                //return View(model);
            }
            foreach (var record in recordsToDelete)
            {
                RavenSession.Delete<Models.Quaker>(record);
            }

            if (model.BatchSize <= model.RemainingRecords)
                model.RemainingRecords = model.RemainingRecords - model.BatchSize;
            else
                model.RemainingRecords = 0;


            if (model.RemainingRecords == 0)
            {
                var uploadedFile = RavenSession.Load<Models.UploadedFile>(model.UploadedFileId);
                uploadedFile.ImportedAt = null;
                uploadedFile.NumberOfRecords = null;

                model.ConfirmRemoval = false;
            }
            stopWatch.Stop();

            model.ExecutionTimeSpan = stopWatch.Elapsed;

            ViewBag.SuccessMessage = string.Format("Remaining records {0}... ", numberOfRecordsToDelete);
            ViewBag.DisbalePageTracking = true;
            return View(model);
        }

    }
}
