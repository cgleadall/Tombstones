using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tombstones.UI.Web.Areas.Tombstones.Models
{
    public class Tombstone
    {
        public string Name {get;set;}
        public string DateOfBirth {get;set;}
        public string DateOfDeath {get;set;}
        public string Site {get;set;}
        public string SearchFields {get;set;}
        public bool PhotoExists {get;set;}
        public bool Found {get;set;}
        public string LocationOnSite {get;set;}
        public string Inscription {get;set;}
        public string Notes { get; set; }


    }
}