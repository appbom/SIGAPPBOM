using System.Web.Mvc;
using BackOffice.Logistica.Cliente.Web.Bootstraper;

namespace SIGAPPBOM.Logistica.Web.Bootstraper
{
    public static class ControllersConfigurator
    {
        public static void Start()
        {
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}