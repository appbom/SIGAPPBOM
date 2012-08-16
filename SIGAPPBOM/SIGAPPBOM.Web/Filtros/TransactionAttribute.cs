using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using StructureMap;

namespace SIGAPPBOM.Web.Filtros
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