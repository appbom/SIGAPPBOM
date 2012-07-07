using System.Web.Mvc;
using SIGAPPBOM.Logistica.Infraestructura.UnitOfWork;
using StructureMap;

namespace SIGAPPBOM.Logistica.Web.Filtros
{
    public class TransactionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
            unitOfWork.Begin();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!(filterContext.Exception != null && !filterContext.ExceptionHandled))
            {
                var unitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
                unitOfWork.End();
            }


        }


    }
}