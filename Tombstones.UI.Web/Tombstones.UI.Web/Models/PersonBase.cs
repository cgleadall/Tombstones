using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tombstones.UI.Web.Models
{
    public class PersonBase
    {
        public string FirstName { get; set; }
        public string OtherNames { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfDeath { get; set; }

        public string CombinedName
        {
            get
            {
                var result = string.Format(@"{0}, {1}", LastName.ToUpper(), FirstName.ToUpper());
                if (!string.IsNullOrEmpty(OtherNames))
                {
                    result += string.Format(@" {0}", OtherNames);
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