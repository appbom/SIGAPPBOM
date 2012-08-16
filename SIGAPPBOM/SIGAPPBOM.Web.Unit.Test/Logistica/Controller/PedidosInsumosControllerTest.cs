using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Servicio.Articulos;
using SIGAPPBOM.Servicio.Comun;
using SIGAPPBOM.Servicio.Pedidos;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Web.Areas.Logistica.Controllers;

namespace SIGAPPBOM.Web.Unit.Test.Logistica.Controller
{
    [TestFixture]
    public class PedidosInsumosControllerTest
    {
        private PedidosInsumosController pedidosInsumosController;
        private Mock<IPedidoInsumosService> pedidoInsumosServiceFalso;
        private Mock<IAuthenticationService> autenticacionServiceFalso;
        private Mock<IArticuloService> articuloServiceFalso;
        private PedidoViewModel pedidoViewModel;
        private List<string> errores;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            pedidoInsumosServiceFalso = new Mock<IPedidoInsumosService>();
            articuloServiceFalso = new Mock<IArticuloService>();
            autenticacionServiceFalso = new Mock<IAuthenticationService>();
            pedidoViewModel = new PedidoViewModel
            {
                Id = 1,
                Estado = Estado.PENDIENTE,
                FechaCreacion = DateTime.Parse("01/02/2012"),
                Descripcion = "Utiles"
            };
            pedidosInsumosController = new PedidosInsumosController(pedidoInsumosServiceFalso.Object, articuloServiceFalso.Object, autenticacionServiceFalso.Object);
            errores = new List<string>();
        }

        [TearDown]
        public void Dispose()
        {
            pedidoInsumosServiceFalso = null;
            pedidosInsumosController = null;
        }

        #endregion

        #region Lista de Pedidos

        [Test]
        [Category("Menu")]
        public void MostrarPedidos_CUANDO_ElUsuarioPedroDiazIngresaAPedidosLogistica_ENTONCES_SeDebeMostrarLaPantallaPedidosLogistica()
        {
            var pedidos = new List<PedidoViewModel>();
            pedidoInsumosServiceFalso.Setup(x => x.TraerLista()).Returns(pedidos);
            var viewResult = (ViewResult)pedidosInsumosController.MostrarPedidos();

            Assert.AreEqual("Pedidos de Insumos", viewResult.ViewBag.Titulo);
        }

        [Test]
        [Category("Ver Lista de Pedidos de Insumos Vacia")]
        public void MostrarPedidos_CUANDO_ElUsuarioPedroDiazIngresaAPedidosLogisticaYNoHayPedidosRegistrados_ENTONCES_SeDebeMostrarLaListaDePedidosVacía()
        {
            var pedidos = new List<PedidoViewModel>();
            pedidoInsumosServiceFalso.Setup(x => x.TraerLista()).Returns(pedidos);

            var viewResult = (ViewResult)pedidosInsumosController.MostrarPedidos();
            var model = (List<PedidoViewModel>)viewResult.Model;

            Assert.AreEqual("Pedidos de Insumos", viewResult.ViewBag.Titulo);
            Assert.AreEqual(0, model.Count);
        }

        [Test]
        [Category("Ver Lista de Pedidos de Insumos Con Elementos")]
        public void MostrarPedidos_CUANDO_ElUsuarioPedroDiazIngresaAPedidosLogisticaYHayDosPedidosRegistrados_ENTONCES_SeDebeMostrarLaListaDePedidosConDosElementos()
        {
            var pedidos = new List<PedidoViewModel>
                              {
                                  new PedidoViewModel{Id = 103, Descripcion = "Pedido 03", Solicitante = "José Díaz",FechaCreacion = DateTime.Parse("10/05/2012")},
                                  new PedidoViewModel{Id = 102, Descripcion = "Pedido 02", Solicitante = "Pedro Flores",FechaCreacion = DateTime.Parse("09/05/2012")},
                                  new PedidoViewModel{Id = 101, Descripcion = "Pedido 01", Solicitante = "Manuel Farfán",FechaCreacion = DateTime.Parse("08/05/2012")}
                              };
            pedidoInsumosServiceFalso.Setup(x => x.TraerLista()).Returns(pedidos);

            var viewResult = (ViewResult)pedidosInsumosController.MostrarPedidos();
            var model = (List<PedidoViewModel>)viewResult.Model;

            Assert.AreEqual("Pedidos de Insumos", viewResult.ViewBag.Titulo);
            Assert.AreEqual(3, model.Count);
        }

        #endregion

        #region Nuevo Pedido

        [Test]
        [Category("Ver Pantalla Nuevo Pedido de Insumos")]
        public void Nuevo_CUANDO_ElUsuarioPedroDiazIngresaPantallaInsumosYPresionaElBotonNuevo_ENTONCES_SeDebeMostrarLaPantallaNuevoPedidoDeInsumos()
        {
            var viewResult = (ViewResult)pedidosInsumosController.Nuevo();
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Nuevo Pedido de Insumos", viewBag.Titulo);
        }

        [Test]
        [Category("Ver la pantalla Buscar Trabajador")]
        public void BuscarTrabajador_CUANDO_IngresoABuscarTrabajadorENTONCES_DeboVerLaPantallaBuscarTrabajador()
        {
            var viewResult = (ViewResult)pedidosInsumosController.BuscarTrabajador();

            Assert.AreEqual("Buscar Trabajador", viewResult.ViewBag.Titulo);
        }

        [Test]
        [Category("Ver la pantalla Búsqueda de Articulo")]
        public void BuscarArticulo_CUANDO_IngresoABuscarArticulo_ENTONCES_DeboVerLaPantallaBuscarArticulo()
        {
            var nombreArticulo = "Lapicero";
            var viewResult = (ViewResult)pedidosInsumosController.BuscarArticulo(nombreArticulo);

            Assert.AreEqual("Búsqueda de Articulo", viewResult.ViewBag.Titulo);
        }

        [Test]
        [Category("Buscar Un Artículo")]
        public void BuscarArticulo_CUANDO_ArticuloNoExiste_ENTONCES_DevuelveUnaListaVacía()
        {
            var nombreArticulo = "Lapicero";
            var articulos = new List<ArticuloViewModel>();

            articuloServiceFalso.Setup(a => a.TraerListaPor(nombreArticulo)).Returns(articulos);
            var viewResult = (ViewResult)pedidosInsumosController.BuscarArticulo(nombreArticulo);
            var listaArticulos = (List<ArticuloViewModel>)(viewResult.Model);

            Assert.AreEqual("Búsqueda de Articulo", viewResult.ViewBag.Titulo);
            Assert.AreEqual(listaArticulos.Count, 0);
        }

        [Test]
        [Category("Buscar Un Artículo")]
        public void BuscarArticulo_CUANDO_ArticuloExiste_ENTONCES_DevuelveUnaListaDeCoincidencias()
        {
            var nombreArticulo = "Lapicero";
            var articulos = new List<ArticuloViewModel>
                                {
                                    new ArticuloViewModel{Id = 1,Nombre = "LAPICERO PILOT TINTA LIQUIDA"},
                                    new ArticuloViewModel{Id = 5,Nombre = "LAPICERO FABER PUNTA FINA"},
                                };

            articuloServiceFalso.Setup(a => a.TraerListaPor(nombreArticulo)).Returns(articulos);
            var viewResult = (ViewResult)pedidosInsumosController.BuscarArticulo(nombreArticulo);
            var listaArticulos = (List<ArticuloViewModel>)(viewResult.Model);

            Assert.AreEqual("Búsqueda de Articulo", viewResult.ViewBag.Titulo);
            Assert.AreEqual(listaArticulos.Count, 2);
        }

        [Test]
        [Category("Grabar un Pedido Con Detalle")]
        public void Nuevo_Cuando_GrabaPedidoConItemLapiceroNegro_Entonces_DeboLlamarAGrabarPedidoyGrabarPedidoConItemLapiceroNegro()
        {
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel
            {
                Item = 1,
                ArticuloId = 1,
                ArticuloNombre = "Lapicero Negro",
                CantidadSolicitada = 1
            });

            pedidoInsumosServiceFalso.Setup(m => m.Grabar(pedidoViewModel)).Returns(true);
            var ruta = (RedirectToRouteResult)pedidosInsumosController.Nuevo(pedidoViewModel);

            Assert.AreEqual("MostrarPedidos", ruta.RouteValues["action"]);

            pedidoInsumosServiceFalso.Verify(m => m.Grabar(pedidoViewModel), Times.AtLeastOnce());
        }

        #endregion

        #region Ver Detalle

        [Test]
        [Category("Ver pantalla Detalle de Pedido de Insumos")]
        public void Detalle_CUANDO_ElUsuarioJuanPerezIngresaADetalleDePedidoYPedidoExiste_ENTONCES_DeboVerPantallaDetalleDePedidoInsumos()
        {
            pedidoInsumosServiceFalso.Setup(x => x.TraerPor(pedidoViewModel.Id)).Returns(pedidoViewModel);

            var viewResult = (ViewResult)pedidosInsumosController.Detalle(pedidoViewModel.Id);
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Detalle de Pedido de Insumos", viewBag.Titulo);
        }

        [Test]
        [Category("Ver pantalla Operación Inválida")]
        public void Detalle_CUANDO_ElUsuarioJuanPerezIngresaADetalleDePedidoYPedidoNoExiste_ENTONCES_DeboVerPantallaOperacionInvalidayMensajeDeError()
        {
            errores.Add("No se puede mostrar detalle - Pedido no existe");
            pedidoInsumosServiceFalso.Setup(x => x.TraerPor(pedidoViewModel.Id)).Returns((PedidoViewModel)null);
            var ruta = (RedirectToRouteResult)pedidosInsumosController.Detalle(pedidoViewModel.Id);

            Assert.AreEqual("Errores", ruta.RouteValues["controller"]);
            Assert.AreEqual("OperacionInvalida", ruta.RouteValues["action"]);
            Assert.AreEqual(errores, pedidosInsumosController.TempData["errores"]);
        }

        #endregion

        #region EliminarPedido

        [Test]
        [Category("Ver pantalla Eliminar de Pedido de Insumos")]
        public void Eliminar_CUANDO_ElUsuarioJuanPerezIngresaAEliminarPedidoYPedidoExiste_ENTONCES_DeboVerPantallaEliminarPedidoInsumos()
        {
            pedidoInsumosServiceFalso.Setup(x => x.TraerPor(pedidoViewModel.Id)).Returns(pedidoViewModel);

            var viewResult = (ViewResult)pedidosInsumosController.Eliminar(pedidoViewModel.Id);
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Eliminar Pedido de Insumos", viewBag.Titulo);
        }

        [Test]
        [Category("Eliminar Pedido de Insumos")]
        public void Eliminar_Cuando_SeHaSeleccionadoElPedido01TieneEstadoProcesadoYSePresionaEliminar_Entonces_DebeMostrarPantallaPedidoNoValidoYDeboVerMensajeEstadoPedidoNoValido()
        {
            errores.Add("No se puede eliminar - Estado de pedido es incorrecto");
            pedidoViewModel.Estado = Estado.ATENDIDO;
            pedidoViewModel.FechaCreacion = DateTime.Parse("01/02/2012");
            pedidoViewModel.FechaAtencion = DateTime.Parse("03/02/2012");
            pedidoInsumosServiceFalso.Setup(x => x.TraerPor(pedidoViewModel.Id)).Returns(pedidoViewModel);

            var rutaDetalle = (RedirectToRouteResult)pedidosInsumosController.Eliminar(pedidoViewModel.Id);

            Assert.AreEqual("OperacionInvalida", rutaDetalle.RouteValues["action"]);
            Assert.AreEqual("Errores", rutaDetalle.RouteValues["controller"]);
            Assert.AreEqual(errores, pedidosInsumosController.TempData["errores"]);
        }

        [Test]
        [Category("Eliminar Pedido de Insumos")]
        public void Eliminar_Cuando_SeHaSeleccionadoElPedido01YSePresionaEliminar_Entonces_DebeMostrarPantallaEliminarPedidoBienesYDeboVerELDetalleDelPedido01()
        {
            pedidoInsumosServiceFalso.Setup(x => x.TraerPor(pedidoViewModel.Id)).Returns(pedidoViewModel);

            var viewResult = (ViewResult)pedidosInsumosController.Eliminar(pedidoViewModel.Id);
            var model = (PedidoViewModel)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Eliminar Pedido de Insumos", viewBag.Titulo);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("Utiles", model.Descripcion);
            Assert.AreEqual(Estado.PENDIENTE, model.Estado);
        }

        [Test]
        [Category("Eliminar Pedido de Insumos")]
        public void EliminarPost_Cuando_OcurrioUnErrorEliminando_Entonces_DebeMostrarPantallaPedidoNoValidoYDeboVerMensajeError()
        {
            errores.Add("No se eliminó el pedido - ERROR ELIMINANDO");
            pedidoInsumosServiceFalso.Setup(x => x.TraerPor(pedidoViewModel.Id)).Returns(pedidoViewModel);
            pedidoInsumosServiceFalso.Setup(x => x.Eliminar(pedidoViewModel.Id)).Returns(false);
            pedidoInsumosServiceFalso.Setup(x => x.Errores).Returns(new List<string> { "No se eliminó el pedido - ERROR ELIMINANDO" });

            var rutaEditarPedido = (RedirectToRouteResult)pedidosInsumosController.EliminarPost(pedidoViewModel.Id);

            Assert.AreEqual("OperacionInvalida", rutaEditarPedido.RouteValues["action"]);
            Assert.AreEqual("Errores", rutaEditarPedido.RouteValues["controller"]);
            Assert.AreEqual(errores, pedidosInsumosController.TempData["errores"]);
        }

        [Test]
        [Category("Eliminar Pedido de Insumos")]
        public void EliminarPost_Cuando_EliminacionRealizadaConExito_Entonces_RedireccionaAMisPedidosInsumos()
        {
            pedidoInsumosServiceFalso.Setup(x => x.TraerPor(pedidoViewModel.Id)).Returns(pedidoViewModel);
            pedidoInsumosServiceFalso.Setup(x => x.Eliminar(pedidoViewModel.Id)).Returns(true);

            var rutaEditarPedido = (RedirectToRouteResult)pedidosInsumosController.EliminarPost(pedidoViewModel.Id);

            Assert.AreEqual("PedidosInsumos", rutaEditarPedido.RouteValues["controller"]);
            Assert.AreEqual("MostrarPedidos", rutaEditarPedido.RouteValues["action"]);
        }

        #endregion

        
    }
}
