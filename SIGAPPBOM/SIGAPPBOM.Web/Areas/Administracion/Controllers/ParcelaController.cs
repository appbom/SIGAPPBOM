using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Web.Filtros;
using SIGAPPBOM.Servicio.Administracion.Parcelas;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Web.Models;

namespace SIGAPPBOM.Web.Areas.Administracion.Controllers
{
    public class ParcelaController : Controller
    {
        private IParcelaService parcelaService;
        private IAuthenticationService authenticationService;

        public ParcelaController(IParcelaService parcelaService, IAuthenticationService authenticationService)
        {
            this.parcelaService = parcelaService;
            this.authenticationService = authenticationService;
        }
        [Transaction]
        public ActionResult MostrarParcela()
        {
            ViewBag.Titulo = "Lista de Parcelas";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();

            var parcelas = parcelaService.TraerTodo();
            if (parcelas.Count == 0)
                ViewBag.Mensaje = "No hay parcelas registradas";

            return View(parcelas);
        }


        public ActionResult Nuevo()
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            ViewBag.Titulo = "Nueva Parcela";
            var parcela = new ParcelaViewModel();
            return View(parcela);
        }

        [Transaction]
        [HttpPost]
        public ActionResult Nuevo(ParcelaViewModel parcela)
        {

            if (ModelState.IsValid)
            {
                if (parcelaService.Grabar(parcela))
                {
                    return RedirectToAction("MostrarParcela");
                }

                ViewBag.Mensaje = parcelaService.Errores[0];
            }

            ViewBag.Titulo = "Nueva Parcela";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(parcela);
        }

        [Transaction]
        public ActionResult Editar(int parcelaId)
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            var errores = new List<string>();
            var parcela = parcelaService.TraerPor(parcelaId);
            if (parcela != null)
            {
                ViewBag.Titulo = "Editar Parcela";
                return View(parcela);
            }
            errores.Add("No se puede mostrar detalle - Parcela no existe");
            return RedirectToAction("OperacionInvalida", "Errores", CrearVistaError(errores));
        }

        [Transaction]
        [HttpPost]
        public ActionResult Editar(ParcelaViewModel parcela)
        {

            if (ModelState.IsValid)
            {
                if (parcelaService.Grabar(parcela))
                {
                    return RedirectToAction("MostrarParcela");
                }

                ViewBag.Mensaje = parcelaService.Errores[0];
            }

            ViewBag.Titulo = "Nueva Parcela";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(parcela);
        }


        #region Metodos Privados

        private OperacionInvalidaViewModel CrearVistaError(List<string> errores)
        {
            TempData["errores"] = errores;
            return new OperacionInvalidaViewModel
            {
                Titulo = "Parcela Inválida",
                Controlador = "Parcelas",
                Accion = "MostrarParcela"
            };
        }

        #endregion

    }
}
