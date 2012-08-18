using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Servicio.Logistica.Almacen;
using SIGAPPBOM.Servicio.Logistica.Pedidos;

namespace SIGAPPBOM.Web.Areas.Logistica.Controllers
{
    public class AlmacenController : Controller
    {
        IAuthenticationService autenticacionService;
        IPedidoInsumosService pedidoInsumosService;
        IAlmacenService almacenService;

        public AlmacenController(IAlmacenService almacenService, IPedidoInsumosService pedidoInsumosService, IAuthenticationService autenticacionService)
        {
            this.almacenService = almacenService;
            this.pedidoInsumosService = pedidoInsumosService;
            this.autenticacionService = autenticacionService;
        }

        public ActionResult MostrarSalidas(int id)
        {
            var salidas = almacenService.TraerSalidas();
            if (salidas.Count == 0)
                ViewBag.Mensaje = "No tiene salidas registradas en el almacen";

            ViewBag.Titulo = "Almacen";
            ViewBag.Accion = "Salidas de Almacen";
            ViewBag.Usuario = autenticacionService.ObtienerInformacionUsuario();
            return View(salidas);
        }
    }
}
