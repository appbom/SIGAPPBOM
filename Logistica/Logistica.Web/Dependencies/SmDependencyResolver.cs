using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace BackOffice.Logistica.Cliente.Web.Dependencies

{
    public class SmDependencyResolver : IDependencyResolver {

        private readonly IContainer container;

        public SmDependencyResolver(IContainer container) {
            this.container = container;
        }

        public object GetService(Type serviceType) {
            
            if (serviceType == null) return null;
            try {
                  return serviceType.IsAbstract || serviceType.IsInterface
                           ? container.TryGetInstance(serviceType)
                           : container.GetInstance(serviceType);
            }
            catch {

                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances<Object>()
                .Where(x => x.GetType() == serviceType);
        }
    }
}