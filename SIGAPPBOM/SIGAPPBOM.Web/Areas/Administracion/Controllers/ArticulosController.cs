using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Servicio.Logistica.Articulos;
using SIGAPPBOM.Web.Filtros;

namespace SIGAPPBOM.Web.Areas.Administracion.Controllers
{
    public class ArticulosController : Controller
    {
        private IArticuloService articuloService;
        private IAuthenticationService authenticationService;

        public ArticulosController(IArticuloService articuloService, IAuthenticationService authenticationService)
        {
            this.articuloService = articuloService;
            this.authenticationService = authenticationService;
        }

        [Transaction]
        public ActionResult MostrarArticulos()
        {
            ViewBag.Titulo = "Lista de Artículos";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();

            var articulos = articuloService.TraerTodo();
            if (articulos.Count == 0)
                ViewBag.Mensaje = "No hay artículos registrados";

            return View(articulos);
        }
    }
}
