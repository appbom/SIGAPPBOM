using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SIGAPPBOM.Logistica.Servicio.Comun;
using SIGAPPBOM.Logistica.Web.Controllers;
using SIGAPPBOM.Logistica.Servicio.Pedidos;
using SIGAPPBOM.Logistica.Servicio.ViewModels;
using SIGAPPBOM.Logistica.Servicio.Articulos;

namespace SIGAPPBOM.Logistica.Web.Unit.Test.Controller
{
    [TestFixture]
    public class PedidosControllerTest
    {
        private PedidosInsumosController pedidosInsumosController;
        private Mock<IPedidoInsumosService> pedidoInsumosServiceFalso;
        private Mock<IArticuloService> articuloServiceFalso;
        private PedidoViewModel pedidoViewModel;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            pedidoInsumosServiceFalso = new Mock<IPedidoInsumosService>();
            articuloServiceFalso = new Mock<IArticuloService>();
            pedidoViewModel = new PedidoViewModel
            {
                Id = 1,
                Estado = Estado.PENDIENTE,
                FechaCreacion = DateTime.Parse("01/02/2012"),
                Descripcion = "Utiles"
            };
            pedidosInsumosController = new PedidosInsumosController(pedidoInsumosServiceFalso.Object, articuloServiceFalso.Object);
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

            Assert.AreEqual("Nuevo Pedido de Insumos", viewResult.ViewBag.Titulo);
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
    }
}
