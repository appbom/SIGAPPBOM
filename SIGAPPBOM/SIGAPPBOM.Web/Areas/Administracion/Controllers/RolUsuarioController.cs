using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Web.Filtros;
using SIGAPPBOM.Servicio.Administracion.RolUsuarios;

namespace SIGAPPBOM.Web.Areas.Administracion.Controllers
{
    public class RolUsuarioController : Controller
    {
        private IRolUsuarioService rolusuarioService;
        private IAuthenticationService authenticationService;

        public RolUsuarioController(IRolUsuarioService rolusuarioService, IAuthenticationService authenticationService)
        {
            this.rolusuarioService = rolusuarioService;
            this.authenticationService = authenticationService;
        }
        [Transaction]
        public ActionResult MostrarRolUsuario()
        {
            ViewBag.Titulo = "Lista de RolUsuarios";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();

            var rolesusuarios = rolusuarioService.TraerTodo();
            if (rolesusuarios.Count == 0)
                ViewBag.Mensaje = "No hay rolesusuarios registrados";

            return View(rolesusuarios);
        }

    }
}
