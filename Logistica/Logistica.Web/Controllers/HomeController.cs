using System.Web.Mvc;
using SIGAPPBOM.Logistica.Infraestructura.Authentication;

namespace SIGAPPBOM.Logistica.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthenticationService authenticationService;

        public HomeController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Módulo de Logística";
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Usuario = authenticationService.ObtienerInformacionUsuario();
            return View();
        }
    }
}
