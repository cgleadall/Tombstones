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
            context.MapRoute(
                "Tombstones_default",
                "Tombstones/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
