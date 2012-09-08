using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Servicio.Administracion.Personas;
using SIGAPPBOM.Web.Filtros;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Web.Models;

namespace SIGAPPBOM.Web.Areas.Administracion.Controllers
{
    public class PersonaController : Controller
    {
        private IAuthenticationService authenticationService;
        private IPersonaService personaService;

      

        public PersonaController(IPersonaService personaService, IAuthenticationService authenticationService)
        {
            // TODO: Complete member initialization
            this.personaService = personaService;
            this.authenticationService = authenticationService;
        }
        //
        // GET: /Administracion/Persona/

        public ActionResult Index()
        {
            return View();
        }

        [Transaction]
        public ActionResult MostrarPersona()
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            ViewBag.Titulo = "Lista de Personas";

            var personas = personaService.TraerTodo();

            if(personas.Count==0)
                ViewBag.Mensaje="No hay Personas registradas";

            return View(personas);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            ViewBag.Titulo = "Nueva Persona";
            var persona = new PersonaViewModel();
            return View(persona);
        }

        [Transaction]
        [HttpPost]
        public ActionResult Nuevo(PersonaViewModel persona)
        {
            
            if (ModelState.IsValid)
            {
                if (personaService.Grabar(persona))
                {
                    return RedirectToAction("MostrarPersona");
                }

                ViewBag.Mensaje = personaService.Errores[0];
            }

            ViewBag.Titulo = "Nueva Persona";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(persona);
        }

        [Transaction]
        public ActionResult Editar(int personaId)
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            var errores = new List<string>();
            var persona = personaService.TraerPor(personaId);
            if (persona != null)
            {
                ViewBag.Titulo = "Editar Persona";
                return View(persona);
            }
            errores.Add("No se puede mostrar detalle - Persona no existe");
            return RedirectToAction("OperacionInvalida", "Errores", CrearVistaError(errores));
        }

        [Transaction]
        [HttpPost]
        public ActionResult Editar(PersonaViewModel persona)
        {

            if (ModelState.IsValid)
            {
                if (personaService.Grabar(persona))
                {
                    return RedirectToAction("MostrarPersona");
                }

                ViewBag.Mensaje = personaService.Errores[0];
            }

            ViewBag.Titulo = "Nueva Persona";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View(persona);
        }


        #region Metodos Privados

        private OperacionInvalidaViewModel CrearVistaError(List<string> errores)
        {
            TempData["errores"] = errores;
            return new OperacionInvalidaViewModel
            {
                Titulo = "Persona Inválida",
                Controlador = "Personas",
                Accion = "MostrarPersona"
            };
        }

        #endregion


    }
}
