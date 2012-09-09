using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Tombstones.UI.Web.ViewModels
{
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