using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Logistica.Infraestructura.Authentication;
using SIGAPPBOM.Logistica.Servicio.Pedidos;
using SIGAPPBOM.Logistica.Servicio.Almacen;

namespace SIGAPPBOM.Logistica.Web.Controllers
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
