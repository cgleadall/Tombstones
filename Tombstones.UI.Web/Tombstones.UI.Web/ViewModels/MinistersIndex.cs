using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tombstones.UI.Web.ViewModels
{
    public class MinistersIndex
    {
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }
        [Display(Name = "starts with")]
        public bool LastnameStartsWith { get; set; }

        [Display(Name = "Firstname")]
        public string Firstname { get; set; }
        [Display(Name = "starts with")]
        public bool FirstnameStartsWith { get; set; }

        public List<Models.Minister> SearchResults { get; set; }
        public int StartAt { get; set; }

        public int NumberOfRecords { get; set; }
    }
}