using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Web.Filtros;
using SIGAPPBOM.Servicio.Administracion.Productores;

namespace SIGAPPBOM.Web.Areas.Administracion.Controllers
{
    public class ProductorController : Controller
    {
        private IProductorService productorService;
        private IAuthenticationService authenticationService;

        public ProductorController(IProductorService productorService, IAuthenticationService authenticationService)
        {
            this.productorService = productorService;
            this.authenticationService = authenticationService;
        }
        [Transaction]
        public ActionResult MostrarProductor()
        {
            ViewBag.Titulo = "Lista de Productores";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();

            var productores = productorService.TraerTodo();
            if (productores.Count == 0)
                ViewBag.Mensaje = "No hay artículos registrados";

            return View(productores);
        }

    }
}
