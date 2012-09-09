using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tombstones.UI.Web.Models
{

    public class UploadedFile
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string Category { get; set; }
        public DateTime UploadedAt { get; set; }
        public DateTime? ImportedAt { get; set; }
        public string ImportedBy { get; set; }
        public int? NumberOfRecords { get; set; }

    }
}