using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tombstones.UI.Web.Models
{
    public class PersonBase
    {
        [Display(Name="First name")]
        public string FirstName { get; set; }

        [Display(Name="Other names", Description="Names between First and Last")]
        public string OtherNames { get; set; }

        [Display(Name="Last name")]
        public string LastName { get; set; }

        [Display(Name="Date of Birth")]
        public string DateOfBirth { get; set; }

        [Display(Name="Date of Death")]
        public string DateOfDeath { get; set; }

        [Display(Name="Full name", Description="LASTNAME, Firstname Other names")]
        public string CombinedName
        {
            get
            {
                string result = string.Empty;

                if (string.IsNullOrEmpty(LastName))
                    return result;

                result = string.Format(@"{0}", LastName.ToUpper());
                if (!string.IsNullOrEmpty(FirstName))
                {
                    result += @", " + FirstName;
                    if (!string.IsNullOrEmpty(OtherNames))
                    {
                        result += string.Format(@" {0}", OtherNames);
                    }
                }
                return result;
            }
        }
        public void SetCombinedName(string combinedName)
        {
            var names = combinedName.Split(",".ToCharArray());
            if (names.Length >= 1)
            {
                LastName = names[0]
                    .Trim()
                    .ToUpper();
            }
            if (names.Length >= 2)
            {
                var allOtherNames = names[1].Trim().Split(" ".ToCharArray());
                if (allOtherNames.Length >= 1)
                {
                    FirstName = allOtherNames[0]
                        .Trim()
                        .ToUpper();
                }
                if (allOtherNames.Length > 1)
                {
                    for (int i = 1; i < allOtherNames.Length; i++)
                    {
                        OtherNames += string.Format(@"{0}{1}", i == 1 ? "" : " ", allOtherNames[i]);
                    }
                }
            }

        }
    }
}