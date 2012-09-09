using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tombstones.UI.Web.ViewModels
{
    public class FileManagerUndoImport
    {
        public string UploadedFileId { get; set; }
        public bool ConfirmRemoval { get; set; }
        public int RemainingRecords { get; set; }
        public int BatchSize { get; set; }
        public TimeSpan ExecutionTimeSpan { get; set; }

        public FileManagerUndoImport()
        {
            BatchSize = 100;
        }
        public static FileManagerUndoImport Create(string uploadedFileId)
        {
            var result = new FileManagerUndoImport();
            result.UploadedFileId = uploadedFileId;
            return result;
        }
    }
}