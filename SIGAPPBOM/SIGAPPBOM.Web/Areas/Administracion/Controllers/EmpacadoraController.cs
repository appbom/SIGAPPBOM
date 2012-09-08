using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Web.Filtros;
using SIGAPPBOM.Servicio.Administracion.Empacadoras;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Web.Models;

namespace SIGAPPBOM.Web.Areas.Administracion.Controllers
{
    public class EmpacadoraController : Controller
    {
        private IEmpacadoraService empacadoraService;
        private IAuthenticationService authenticationService;

        public EmpacadoraController(IEmpacadoraService empacadoraService, IAuthenticationService authenticationService)
        {
            this.empacadoraService = empacadoraService;
            this.authenticationService = authenticationService;
        }
        [Transaction]
        public ActionResult MostrarEmpacadora()
        {
            ViewBag.Titulo = "Lista de Empacadoras";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();

            var empacadoras = empacadoraService.TraerTodo();
            if (empacadoras.Count == 0)
                ViewBag.Mensaje = "No hay empacadoras registradas";

            return View(empacadoras);
        }


        public ActionResult Nuevo()
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            ViewBag.Titulo = "Nueva Empacadora";
            var empacadora = new EmpacadoraViewModel();
            return View(empacadora);
        }

        [Transaction]
        [HttpPost]
        public ActionResult Nuevo(EmpacadoraViewModel empacadora)
        {

            if (ModelState.IsValid)
            {
                if (empacadoraService.Grabar(empacadora))
                {
                    return RedirectToAction("MostrarEmpacadora");
                }

                ViewBag.Mensaje = empacadoraService.Errores[0];
            }

            ViewBag.Titulo = "Nueva Empacadora";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(empacadora);
        }

        [Transaction]
        public ActionResult Editar(int empacadoraId)
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            var errores = new List<string>();
            var empacadora = empacadoraService.TraerPor(empacadoraId);
            if (empacadora != null)
            {
                ViewBag.Titulo = "Editar Empacadora";
                return View(empacadora);
            }
            errores.Add("No se puede mostrar detalle - Empacadora no existe");
            return RedirectToAction("OperacionInvalida", "Errores", CrearVistaError(errores));
        }

        [Transaction]
        [HttpPost]
        public ActionResult Editar(EmpacadoraViewModel empacadora)
        {

            if (ModelState.IsValid)
            {
                if (empacadoraService.Grabar(empacadora))
                {
                    return RedirectToAction("MostrarEmpacadora");
                }

                ViewBag.Mensaje = empacadoraService.Errores[0];
            }

            ViewBag.Titulo = "Nueva Empacadora";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(empacadora);
        }


        #region Metodos Privados

        private OperacionInvalidaViewModel CrearVistaError(List<string> errores)
        {
            TempData["errores"] = errores;
            return new OperacionInvalidaViewModel
            {
                Titulo = "Empacadora Inválida",
                Controlador = "Empacadoras",
                Accion = "MostrarEmpacadora"
            };
        }

        #endregion

    }
}
