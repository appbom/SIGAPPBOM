using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;

namespace SIGAPPBOM.Web.Controllers
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
            ViewBag.Message = "SIGAPPBOM";
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
