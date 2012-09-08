using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Web.Filtros;
using SIGAPPBOM.Servicio.Administracion.Destinos;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Web.Models;

namespace SIGAPPBOM.Web.Areas.Administracion.Controllers
{
    public class DestinoController : Controller
    {
        private IDestinoService destinoService;
        private IAuthenticationService authenticationService;

        public DestinoController(IDestinoService destinoService, IAuthenticationService authenticationService)
        {
            this.destinoService = destinoService;
            this.authenticationService = authenticationService;
        }
        [Transaction]
        public ActionResult MostrarDestino()
        {
            ViewBag.Titulo = "Lista de Destinos";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();

            var destinos = destinoService.TraerTodo();
            if (destinos.Count == 0)
                ViewBag.Mensaje = "No hay destinos registrados";

            return View(destinos);
        }



        public ActionResult Nuevo()
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            ViewBag.Titulo = "Nueva Destino";
            var destino = new DestinoViewModel();
            return View(destino);
        }

        [Transaction]
        [HttpPost]
        public ActionResult Nuevo(DestinoViewModel destino)
        {

            if (ModelState.IsValid)
            {
                if (destinoService.Grabar(destino))
                {
                    return RedirectToAction("MostrarDestino");
                }

                ViewBag.Mensaje = destinoService.Errores[0];
            }

            ViewBag.Titulo = "Nueva Destino";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(destino);
        }

        [Transaction]
        public ActionResult Editar(int destinoId)
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            var errores = new List<string>();
            var destino = destinoService.TraerPor(destinoId);
            if (destino != null)
            {
                ViewBag.Titulo = "Editar Destino";
                return View(destino);
            }
            errores.Add("No se puede mostrar detalle - Destino no existe");
            return RedirectToAction("OperacionInvalida", "Errores", CrearVistaError(errores));
        }

        [Transaction]
        [HttpPost]
        public ActionResult Editar(DestinoViewModel destino)
        {

            if (ModelState.IsValid)
            {
                if (destinoService.Grabar(destino))
                {
                    return RedirectToAction("MostrarDestino");
                }

                ViewBag.Mensaje = destinoService.Errores[0];
            }

            ViewBag.Titulo = "Nueva Destino";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(destino);
        }


        #region Metodos Privados

        private OperacionInvalidaViewModel CrearVistaError(List<string> errores)
        {
            TempData["errores"] = errores;
            return new OperacionInvalidaViewModel
            {
                Titulo = "Destino Inválido",
                Controlador = "Destinos",
                Accion = "MostrarDestino"
            };
        }

        #endregion

    }
}