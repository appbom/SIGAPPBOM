using System.Web.Mvc;
using SIGAPPBOM.Web.Dependencies;
using StructureMap;

namespace SIGAPPBOM.Web.Bootstraper
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