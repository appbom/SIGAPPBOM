using System.Web.Mvc;
using BackOffice.Logistica.Cliente.Web.Dependencies;
using SIGAPPBOM.Logistica.Web.Dependencies;
using StructureMap;

namespace SIGAPPBOM.Logistica.Web.Bootstraper
{
    public static class DependencyConfigurator
    {
        public static void Start()
        {
            var container = (IContainer)IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}