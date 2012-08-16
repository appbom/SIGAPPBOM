using System.Web.Mvc;

namespace SIGAPPBOM.Web.Bootstraper
{
    public static class ControllersConfigurator
    {
        public static void Start()
        {
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}