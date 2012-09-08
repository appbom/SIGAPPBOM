using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Web.Filtros;
using SIGAPPBOM.Servicio.Administracion.Roles;
using SIGAPPBOM.Web.Models;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Web.Areas.Administracion.Controllers
{
    public class RolController : Controller
    {
        private IRolService rolService;
        private IAuthenticationService authenticationService;

        public RolController(IRolService rolService, IAuthenticationService authenticationService)
        {
            this.rolService = rolService;
            this.authenticationService = authenticationService;
        }
        [Transaction]
        public ActionResult MostrarRol()
        {
            ViewBag.Titulo = "Lista de Roles";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();

            var roles = rolService.TraerTodo();
            if (roles.Count == 0)
                ViewBag.Mensaje = "No hay roles registrados";

            return View(roles);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            ViewBag.Titulo = "Nuevo Rol";
            var rol = new RolViewModel();
            return View(rol);
        }

        [Transaction]
        [HttpPost]
        public ActionResult Nuevo(RolViewModel rol)
        {

            if (ModelState.IsValid)
            {
                if (rolService.Grabar(rol))
                {
                    return RedirectToAction("MostrarRol");
                }

                ViewBag.Mensaje = rolService.Errores[0];
            }

            ViewBag.Titulo = "Nuevo Rol";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(rol);
        }

        [Transaction]
        public ActionResult Editar(int rolId)
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            var errores = new List<string>();
            var rol = rolService.TraerPor(rolId);
            if (rol != null)
            {
                ViewBag.Titulo = "Editar Rol";
                return View(rol);
            }
            errores.Add("No se puede mostrar detalle - Rol no existe");
            return RedirectToAction("OperacionInvalida", "Errores", CrearVistaError(errores));
        }

        [Transaction]
        [HttpPost]
        public ActionResult Editar(RolViewModel rol)
        {

            if (ModelState.IsValid)
            {
                if (rolService.Grabar(rol))
                {
                    return RedirectToAction("MostrarRol");
                }

                ViewBag.Mensaje = rolService.Errores[0];
            }

            ViewBag.Titulo = "Nuevo Rol";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(rol);
        }


        #region Metodos Privados

        private OperacionInvalidaViewModel CrearVistaError(List<string> errores)
        {
            TempData["errores"] = errores;
            return new OperacionInvalidaViewModel
            {
                Titulo = "Rol Inválido",
                Controlador = "Roles",
                Accion = "MostrarRol"
            };
        }

        #endregion

    }
}