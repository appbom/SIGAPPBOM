using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Servicio.Articulos;
using SIGAPPBOM.Servicio.Comun;
using SIGAPPBOM.Servicio.Pedidos;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Web.Filtros;
using SIGAPPBOM.Web.Models;

namespace SIGAPPBOM.Web.Areas.Logistica.Controllers
{
    public class PedidosInsumosController : Controller
    {
        private IPedidoInsumosService pedidoInsumosService;
        private IArticuloService articuloService;
        private IAuthenticationService autenticacionService;

        public PedidosInsumosController(IPedidoInsumosService iPedidoInsumosService, IArticuloService iArticuloService, IAuthenticationService autenticacionService)
        {
            // TODO: Complete member initialization
            this.pedidoInsumosService = iPedidoInsumosService;
            this.articuloService = iArticuloService;
            this.autenticacionService = autenticacionService;
        }

        [Transaction]
        public ActionResult MostrarPedidos()
        {
            var pedidos = pedidoInsumosService.TraerLista().OrderBy(x => x.FechaCreacion).ToList();
            ViewBag.Titulo = "Pedidos de Insumos";
            ViewBag.Usuario = autenticacionService.ObtienerInformacionUsuario();
            return View(pedidos);
        }

        public ActionResult Nuevo()
        {
            ViewBag.Usuario = autenticacionService.ObtienerInformacionUsuario();
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
            ViewBag.Usuario = autenticacionService.ObtienerInformacionUsuario();
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

        [Transaction]
        public ActionResult Detalle(int pedidoId)
        {
            ViewBag.Usuario = autenticacionService.ObtienerInformacionUsuario();
            var errores = new List<string>();
            var pedido = pedidoInsumosService.TraerPor(pedidoId);
            if (pedido != null)
            {
                ViewBag.Titulo = "Detalle de Pedido de Insumos";
                return View(pedido);
            }
            errores.Add("No se puede mostrar detalle - Pedido no existe");
            return RedirectToAction("OperacionInvalida", "Errores", CrearVistaError(errores));
        }

        [Transaction]
        public ActionResult Eliminar(int pedidoId)
        {
            var errores = new List<string>();
            var pedido = pedidoInsumosService.TraerPor(pedidoId);

            if (pedido == null)
                errores.Add("No se puede eliminar - Pedido no existe");
            else
            {
                if (pedido.Estado != Estado.PENDIENTE)
                    errores.Add("No se puede eliminar - Estado de pedido es incorrecto");

                ViewBag.Titulo = "Eliminar Pedido de Insumos";
                ViewBag.Usuario = autenticacionService.ObtienerInformacionUsuario();

                if (errores.Count == 0)
                    return View(pedido);
            }

            return RedirectToAction("OperacionInvalida", "Errores", CrearVistaError(errores));
        }

        [Transaction]
        public ActionResult EliminarPost(int pedidoId)
        {
            if (pedidoInsumosService.Eliminar(pedidoId))
                return RedirectToAction("MostrarPedidos", "PedidosInsumos");

            return RedirectToAction("OperacionInvalida", "Errores", CrearVistaError(pedidoInsumosService.Errores));

        }

        #region Metodos Privados

        private OperacionInvalidaViewModel CrearVistaError(List<string> errores)
        {
            TempData["errores"] = errores;
            return new OperacionInvalidaViewModel
            {
                Titulo = "Pedido Inválido",
                Controlador = "PedidosInsumos",
                Accion = "MostrarPedidos"
            };
        }

        #endregion
    }
}
