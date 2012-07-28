using System;
using System.Linq;
using System.Web.Mvc;
using SIGAPPBOM.Logistica.Servicio.Articulos;
using SIGAPPBOM.Logistica.Servicio.Pedidos;
using SIGAPPBOM.Logistica.Servicio.ViewModels;
using SIGAPPBOM.Logistica.Web.Filtros;

namespace SIGAPPBOM.Logistica.Web.Controllers
{
    public class PedidosInsumosController : Controller
    {
        private IPedidoInsumosService pedidoInsumosService;
        private IArticuloService articuloService;

        public PedidosInsumosController(IPedidoInsumosService iPedidoInsumosService, IArticuloService iArticuloService)
        {
            // TODO: Complete member initialization
            this.pedidoInsumosService = iPedidoInsumosService;
            this.articuloService = iArticuloService;
        }

        [Transaction]
        public ActionResult MostrarPedidos()
        {
            var pedidos = pedidoInsumosService.TraerLista().OrderBy(x => x.FechaCreacion).ToList();
            ViewBag.Titulo = "Pedidos de Insumos";
            return View(pedidos);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Titulo = "Nuevo Pedido de Insumos";
            var pedido = new PedidoViewModel();
            return View(pedido);
        }

        [Transaction]
        [HttpPost]
        public ActionResult Nuevo(PedidoViewModel pedido)
        {
            pedido.FechaCreacion = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (pedidoInsumosService.Grabar(pedido))
                {
                    return RedirectToAction("MostrarPedidos");
                }

                ViewBag.Mensaje = pedidoInsumosService.Errores[0];
            }

            ViewBag.Titulo = "Nuevo Pedido de Insumos";

            return View(pedido);
        }

        public ActionResult BuscarTrabajador()
        {
            ViewBag.Titulo = "Buscar Trabajador";
            return View();
        }

        [Transaction]
        public ActionResult BuscarArticulo(string nombreArticulo)
        {
            ViewBag.Titulo = "Búsqueda de Articulo";
            var articulos = articuloService.TraerListaPor(nombreArticulo);
            return View(articulos);

        }
    }
}
