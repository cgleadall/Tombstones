using System.Web.Mvc;

namespace Tombstones.UI.Web.Areas.Research
{
    public class ResearchAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Research";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Research_default",
                url: "Research/{controller}/{action}/{id}",
                defaults: new { controller="Resources", action = "index", id = UrlParameter.Optional },
                namespaces: new [] {"Tombstones.UI.Web.Areas.Research.Controllers"}
            );
        }
    }
}
