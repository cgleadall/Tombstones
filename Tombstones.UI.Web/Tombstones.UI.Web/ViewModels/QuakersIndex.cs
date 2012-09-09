using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tombstones.UI.Web.ViewModels
{
    public class QuakersIndex
    {
        public string Surname { get; set; }
        public bool SurnameStartsWith { get; set; }

        public string Firstname { get; set; }
        public bool FirstnameStartsWith { get; set; }

        public List<Models.Quaker> SearchResults { get; set; }
        public int NumberOfRecords { get; set; }
    }
}