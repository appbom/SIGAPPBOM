using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIGAPPBOM.Logistica.Servicio.Pedidos;
using SIGAPPBOM.Logistica.Web.Filtros;

namespace SIGAPPBOM.Logistica.Web.Controllers
{
    public class PedidosController : Controller
    {
        private IPedidoBienesService pedidoBienesService;

        public PedidosController(IPedidoBienesService IPedidoBienesService)
        {
            // TODO: Complete member initialization
            this.pedidoBienesService = IPedidoBienesService;
        }

        [Transaction]
        public ActionResult PedidosLogistica()
        {
            var pedidos = pedidoBienesService.TraerLista().OrderBy(x => x.FechaCreacion).ToList();
            ViewBag.Titulo = "Pedidos Logística";
            return View(pedidos);
        }
    }
}
