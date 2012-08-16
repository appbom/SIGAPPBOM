using System.Collections.Generic;
using System.Web.Mvc;
using SIGAPPBOM.Web.Models;

namespace SIGAPPBOM.Web.Controllers
{
    public class ErroresController : Controller
    {

        public ActionResult OperacionInvalida(OperacionInvalidaViewModel operacionInvalida)
        {
            var mensajes = (List<string>)TempData["errores"];
            operacionInvalida.Mensajes = mensajes;
            return View(operacionInvalida);
        }
    }
}
