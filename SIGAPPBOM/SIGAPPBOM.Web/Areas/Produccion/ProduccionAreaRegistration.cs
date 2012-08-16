using System.Web.Mvc;

namespace SIGAPPBOM.Web.Areas.Produccion
{
    public class ProduccionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Produccion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Produccion_default",
                "Produccion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
