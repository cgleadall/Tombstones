using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tombstones.UI.Web.Models
{
    public class HomeModel
    {
        [Display(Name = "Last Updated:")]
        public DateTime LastUpdateTime { get; set; }
    }
}