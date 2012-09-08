using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Web.Filtros;
using SIGAPPBOM.Servicio.Administracion.Usuarios;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Web.Models;

namespace SIGAPPBOM.Web.Areas.Administracion.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioService usuarioService;
        private IAuthenticationService authenticationService;

        public UsuarioController(IUsuarioService usuarioService, IAuthenticationService authenticationService)
        {
            this.usuarioService = usuarioService;
            this.authenticationService = authenticationService;
        }
        [Transaction]
        public ActionResult MostrarUsuario()
        {
            ViewBag.Titulo = "Lista de Usuarios";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();

            var usuarios = usuarioService.TraerTodo();
            if (usuarios.Count == 0)
                ViewBag.Mensaje = "No hay usuarios registrados";

            return View(usuarios);
        }


        public ActionResult Nuevo()
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            ViewBag.Titulo = "Nuevo Usuario";
            var usuario = new UsuarioViewModel();
            return View(usuario);
        }

        [Transaction]
        [HttpPost]
        public ActionResult Nuevo(UsuarioViewModel usuario)
        {

            if (ModelState.IsValid)
            {
                if (usuarioService.Grabar(usuario))
                {
                    return RedirectToAction("MostrarUsuario");
                }

                ViewBag.Mensaje = usuarioService.Errores[0];
            }

            ViewBag.Titulo = "Nuevo Usuario";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(usuario);
        }

        [Transaction]
        public ActionResult Editar(int usuarioId)
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            var errores = new List<string>();
            var usuario = usuarioService.TraerPor(usuarioId);
            if (usuario != null)
            {
                ViewBag.Titulo = "Editar Usuario";
                return View(usuario);
            }
            errores.Add("No se puede mostrar detalle - Usuario no existe");
            return RedirectToAction("OperacionInvalida", "Errores", CrearVistaError(errores));
        }

        [Transaction]
        [HttpPost]
        public ActionResult Editar(UsuarioViewModel usuario)
        {

            if (ModelState.IsValid)
            {
                if (usuarioService.Grabar(usuario))
                {
                    return RedirectToAction("MostrarUsuario");
                }

                ViewBag.Mensaje = usuarioService.Errores[0];
            }

            ViewBag.Titulo = "Nuevo Usuario";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(usuario);
        }


        #region Metodos Privados

        private OperacionInvalidaViewModel CrearVistaError(List<string> errores)
        {
            TempData["errores"] = errores;
            return new OperacionInvalidaViewModel
            {
                Titulo = "Usuario Inválido",
                Controlador = "Usuarios",
                Accion = "MostrarUsuario"
            };
        }

        #endregion

    }
}
