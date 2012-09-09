using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tombstones.UI.Web.ViewModels
{
    public class QuakersIndex
    {
        [Display(Name="Surname")]
        public string Surname { get; set; }
        [Display(Name="starts with")]
        public bool SurnameStartsWith { get; set; }

        [Display(Name="Firstname")]
        public string Firstname { get; set; }
        [Display(Name="starts with")]
        public bool FirstnameStartsWith { get; set; }

        public List<Models.Quaker> SearchResults { get; set; }
        public int NumberOfRecords { get; set; }
    }
}