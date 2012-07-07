namespace BackOffice.Logistica.Cliente.Web.Bootstraper
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using StructureMap;

    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                var controllerType = base.GetControllerType(requestContext, controllerName);
                return ObjectFactory.GetInstance(controllerType) as IController;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(ObjectFactory.Container.WhatDoIHave());
                throw;
               
            }
        }
/*        
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType==null) return null;
            
            try
            {
                return ObjectFactory.GetInstance(controllerType) as Controller;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(ObjectFactory.Container.WhatDoIHave());
                throw;
            }
        }*/
    }
}