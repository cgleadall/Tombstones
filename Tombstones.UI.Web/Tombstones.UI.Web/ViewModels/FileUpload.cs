using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tombstones.UI.Web.Models;

namespace Tombstones.UI.Web.ViewModels
{
    public class FileUpload
    {

        public ICollection<SelectListItem> FileCategories { get; set; }
        public string SelectedFileCategory { get; set; }

        public UploadedFile FileBeingUploaded { get; set; }

        public FileUpload()
        {
            FileCategories = new List<SelectListItem>();

            foreach (var typeName in new string[] { "Burials", "Plantations", "Quakers", "Tombstones"})
            {
                FileCategories.Add(new SelectListItem { Text = typeName, Value = typeName });
            }

        }
    }
}