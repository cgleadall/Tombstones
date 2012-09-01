using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tombstones.UI.Web.Areas.Research.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public IList<string> Addresses { get; set; }

        public Resource()
        {
            Addresses = new List<string>();
        }
    }

    public class ResourceCollection
    {
        public IList<Resource> Resources { get; set; }

        public ResourceCollection()
        {
            Resources = new List<Resource>();

            SeedCollection();
        }

        private void SeedCollection()
        {
            // Antigua
            var item = new Resource{
                Country = "Antigua",
                Title = "Antigua & Barbuda National Archives"
            };
            item.Addresses.Add("Victoria Park, Factory Road, St. John's, Antigua");
            item.Addresses.Add("1-268- 462 -3946 / 7");
            item.Addresses.Add("archives@candw.ag");

            Resources.Add(item);
        }
    }
}