using System.Web.Mvc;

namespace Tombstones.UI.Web.Areas.Tombstones
{
    public class TombstonesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Tombstones";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            var namespaces = new string[] { "Tombstones.UI.Web.Areas.Tombstones.Controlers" };

            context.MapRoute(
                name: "Tombstones_default",
                url: "Tombstones/{controller}/{action}/{id}",
                defaults: new {controller = "Home",  action = "Index", id = UrlParameter.Optional },
                namespaces: new [] { "Tombstones.UI.Web.Areas.Tombstones.Controllers"}
            );
        }
    }
}
