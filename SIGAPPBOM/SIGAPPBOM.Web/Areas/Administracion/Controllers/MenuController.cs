using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Web.Filtros;
using SIGAPPBOM.Servicio.Administracion.Menues;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Web.Models;

namespace SIGAPPBOM.Web.Areas.Administracion.Controllers
{
    public class MenuController : Controller
    {
        private IMenuService menuService;
        private IAuthenticationService authenticationService;

        public MenuController(IMenuService menuService, IAuthenticationService authenticationService)
        {
            this.menuService = menuService;
            this.authenticationService = authenticationService;
        }
        [Transaction]
        public ActionResult MostrarMenu()
        {
            ViewBag.Titulo = "Lista de Menues";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();

            var menues = menuService.TraerTodo();
            if (menues.Count == 0)
                ViewBag.Mensaje = "No hay menues registrados";

            return View(menues);
        }


        public ActionResult Nuevo()
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            ViewBag.Titulo = "Nuevo Menu";
            var menu = new MenuViewModel();
            return View(menu);
        }

        [Transaction]
        [HttpPost]
        public ActionResult Nuevo(MenuViewModel menu)
        {

            if (ModelState.IsValid)
            {
                if (menuService.Grabar(menu))
                {
                    return RedirectToAction("MostrarMenu");
                }

                ViewBag.Mensaje = menuService.Errores[0];
            }

            ViewBag.Titulo = "Nuevo Menu";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(menu);
        }

        [Transaction]
        public ActionResult Editar(int menuId)
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            var errores = new List<string>();
            var menu = menuService.TraerPor(menuId);
            if (menu != null)
            {
                ViewBag.Titulo = "Editar Menu";
                return View(menu);
            }
            errores.Add("No se puede mostrar detalle - Menu no existe");
            return RedirectToAction("OperacionInvalida", "Errores", CrearVistaError(errores));
        }

        [Transaction]
        [HttpPost]
        public ActionResult Editar(MenuViewModel menu)
        {

            if (ModelState.IsValid)
            {
                if (menuService.Grabar(menu))
                {
                    return RedirectToAction("MostrarMenu");
                }

                ViewBag.Mensaje = menuService.Errores[0];
            }

            ViewBag.Titulo = "Nuevo Menu";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(menu);
        }


        #region Metodos Privados

        private OperacionInvalidaViewModel CrearVistaError(List<string> errores)
        {
            TempData["errores"] = errores;
            return new OperacionInvalidaViewModel
            {
                Titulo = "Menu Inválido",
                Controlador = "Menues",
                Accion = "MostrarMenu"
            };
        }

        #endregion

    }
}