﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
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

    public class FileManagerIndex
    {
        public ICollection<Models.UploadedFile> UploadedFiles { get; set; }
        public ICollection<Models.UploadedFile> ImportedFiles { get; set; }
        public ICollection<FileInfo> FilesInUploadFolder { get; set; }

        public static FileManagerIndex Create(string baseUploadFolder)
        {
            var result = new FileManagerIndex();
            result.UploadedFiles = new List<Models.UploadedFile>();
            result.FilesInUploadFolder = new List<FileInfo>();

            var uploadFolder = baseUploadFolder;

            var directoryInfo = new DirectoryInfo(uploadFolder);

            foreach (DirectoryInfo directory in directoryInfo.EnumerateDirectories())
            {
                result.FilesInUploadFolder = GetFilesFromDirectory(directory.FullName).ToList<FileInfo>();
            }
            return result;
        }

        protected static ICollection<FileInfo> GetFilesFromDirectory(string directoryName)
        {
            var result = new List<FileInfo>();
            var di = new DirectoryInfo(directoryName);
            if (!di.Exists)
                return result;

            foreach (var file in di.EnumerateFiles())
            {
                result.Add(file);
            }

            return result;

        }
    }
}