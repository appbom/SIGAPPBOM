using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.Dominio.Pedidos;
using SIGAPPBOM.Logistica.NHibernate.Repositorios;
using SIGAPPBOM.Logistica.Servicio.Comun;
using SIGAPPBOM.Logistica.Servicio.Pedidos;
using SIGAPPBOM.Logistica.Servicio.ViewModels;

namespace SIGAPPBOM.Logistica.Servicio.Unit.Test.Pedidos
{
    [TestFixture]
    public class PedidoBienesServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Pedido>> pedidosRepositorioFalso;
        private Mock<IRepositorio<Articulo>> articulosRepositorioFalso;
        private PedidoInsumosService pedidoBienesService;
        private PedidoViewModel pedidoViewModel;
        private Pedido pedidoModelo;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            pedidosRepositorioFalso = new Mock<IRepositorio<Pedido>>();
            articulosRepositorioFalso = new Mock<IRepositorio<Articulo>>();
            pedidoBienesService = new PedidoInsumosService(pedidosRepositorioFalso.Object, articulosRepositorioFalso.Object, mappingEngine);
            pedidoViewModel = new PedidoViewModel { Id = 1, Descripcion = "Pedido 01", Solicitante = "Pedro Dominguez", Estado = Estado.PENDIENTE, FechaCreacion = DateTime.Parse("12/05/2012") };
            pedidoModelo = new Pedido { Id = 1, Descripcion = "Pedido 01", Solicitante = "Pedro Dominguez", Estado = Estado.PENDIENTE.GetHashCode(), FechaCreacion = DateTime.Parse("12/05/2012") };
        }

        [TearDown]
        public void Dispose()
        {
            pedidosRepositorioFalso = null;
            articulosRepositorioFalso = null;
            pedidoBienesService = null;
        }

        #endregion

        #region TraerLista

        [Test]
        public void TraerLista_CUANDO_NoExistenPedidosDeBienes_ENTONCES_DevuelveUnaListaVacía()
        {
            var pedidos = new EnumerableQuery<Pedido>(new List<Pedido>());

            pedidosRepositorioFalso.Setup(x => x.TraerTodo()).Returns(pedidos);

            var pedidosViewModel = pedidoBienesService.TraerLista();

            Assert.AreEqual(0, pedidosViewModel.Count);
            Assert.AreEqual("No hay pedidos de bienes registrados", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void TraerLista_CUANDO_ExistenDosPedidosDeBienes_ENTONCES_DevuelveUnaListaConDosPedidos()
        {
            var pedidos = new EnumerableQuery<Pedido>(new List<Pedido>
                                                          {
                                                              new Pedido{Id = 1,Descripcion = "Pedido 01",Solicitante = "Pedro Dominguez", Estado= Estado.PENDIENTE.GetHashCode(), FechaCreacion = DateTime.Parse("12/05/2012")},
                                                              new Pedido{Id = 2,Descripcion = "Pedido 02",Solicitante = "Juan Aquino", Estado= Estado.PENDIENTE.GetHashCode(), FechaCreacion = DateTime.Parse("10/05/2012")},
                                                          });

            pedidosRepositorioFalso.Setup(x => x.TraerTodo()).Returns(pedidos);

            var pedidosViewModel = pedidoBienesService.TraerLista();

            Assert.AreEqual(2, pedidosViewModel.Count);
        }

        #endregion

        #region Grabar

        [Test]
        public void Grabar_Cuando_SolcitanteEsVacio_Entonces_RetornarFalsoYMensajeError_IngresarSolicitanteParaPedido()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Solicitante = "";
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel());

            Assert.IsFalse(pedidoBienesService.Grabar(pedidoViewModel));
            Assert.AreEqual("Ingresar solicitante para pedido", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Grabar_Cuando_DescripcionDePedidoVacia_Entonces_RetornarFalsoYMensajeError_IngresarDescripcionDePedido()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Solicitante = "Pedro Perez";
            pedidoViewModel.Descripcion = "";
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel());

            Assert.IsFalse(pedidoBienesService.Grabar(pedidoViewModel));
            Assert.AreEqual("Ingresar descripción de pedido", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Grabar_Cuando_DetalleDePedidoEstaVacio_Entonces_RetornarFalsoYMensajeError_IngresarDetalleDePedido()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Solicitante = "Pedro Perez";
            Assert.IsFalse(pedidoBienesService.Grabar(pedidoViewModel));
            Assert.AreEqual("Añadir artículos al detalle de pedido", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Grabar_Cuando_CantidadPedidaEsMenorACero_Entonces_RetornarFalsoYMensajeError_IngresarCantidadMayorACero()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Solicitante = "Pedro Perez";
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 101, ArticuloNombre = "Articulo", CantidadSolicitada = 0 });

            Assert.IsFalse(pedidoBienesService.Grabar(pedidoViewModel));
            Assert.AreEqual("Item 1: Ingresar cantidad mayor a cero", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Grabar_Cuando_ArticuloNoExiste_Entonces_RetornarFalsoYMensajeError_ArticuloNoExiste()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1000, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            int articuloId = 1;
            Articulo articulo = null;
            articulosRepositorioFalso.Setup(s => s.BuscarPor(articuloId)).Returns(articulo);

            Assert.IsFalse(pedidoBienesService.Grabar(pedidoViewModel));
            Assert.AreEqual("Item 1: Articulo no existe", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Grabar_Cuando_NoExistanErrores_Entonces_RetornarTrueYCantidadErroresCero()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            Articulo articulo = new Articulo { Id = 1 };
            articulosRepositorioFalso.Setup(s => s.BuscarPor(articulo.Id)).Returns(articulo);

            Assert.IsTrue(pedidoBienesService.Grabar(pedidoViewModel));
            Assert.AreEqual(0, pedidoBienesService.Errores.Count);
        }

        #endregion

        #region Actualizar

        [Test]
        public void Actualizar_Cuando_PedidoNoExiste_Entonces_RetornarFalsoYMensajeError_PedidoNoExiste()
        {
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(101)).Returns((Pedido)null);

            pedidoViewModel.Solicitante = string.Empty;

            Assert.IsFalse(pedidoBienesService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Pedido no existe", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Actualizar_Cuando_SolcitanteEsVacio_Entonces_RetornarFalsoYMensajeError_IngresarSolicitanteParaPedido()
        {
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            pedidoViewModel.Solicitante = string.Empty;

            Assert.IsFalse(pedidoBienesService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Ingresar solicitante para pedido", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Actualizar_Cuando_DescripcionDePedidoVacia_Entonces_RetornarFalsoYMensajeError_IngresarDescripcionDePedido()
        {
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            pedidoViewModel.Descripcion = "";

            Assert.IsFalse(pedidoBienesService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Ingresar descripción de pedido", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Actualizar_Cuando_DetalleDePedidoEstaVacio_Entonces_RetornarFalsoYMensajeError_IngresarDetalleDePedido()
        {
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            Assert.IsFalse(pedidoBienesService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Añadir artículos al detalle de pedido", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Actualizar_Cuando_CantidadPedidaEsMenorACero_Entonces_RetornarFalsoYMensajeError_IngresarCantidadMayorACero()
        {
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            pedidoViewModel.Detalles.Clear();
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 101, ArticuloNombre = "Articulo", CantidadSolicitada = 0 });

            Assert.IsFalse(pedidoBienesService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Item 1: Ingresar cantidad mayor a cero", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Actualizar_Cuando_ArticuloNoExiste_Entonces_RetornarFalsoYMensajeError_ArticuloNoExiste()
        {
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            pedidoViewModel.Detalles.Clear();
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 101, ArticuloNombre = "Articulo", CantidadSolicitada = 20 });

            articulosRepositorioFalso.Setup(s => s.BuscarPor(101)).Returns((Articulo)null);

            Assert.IsFalse(pedidoBienesService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Item 1: Articulo no existe", pedidoBienesService.Errores[0]);
        }

        [Test]
        public void Actualizar_Cuando_PedidoEditadoCorrectamente_Entonces_RetornarTrueYCantidadErroresCero()
        {
            var articulo = new Articulo { Id = 101, Nombre = "Articulo 2", CodigoCatalogo = "A2", Stock = 500 };

            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            pedido.Descripcion = "Descripcion editada";
            pedidoViewModel.Detalles.Clear();
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = articulo.Id, ArticuloNombre = articulo.Nombre, CantidadSolicitada = 20 });

            articulosRepositorioFalso.Setup(s => s.BuscarPor(articulo.Id)).Returns(articulo);

            Assert.IsTrue(pedidoBienesService.Actualizar(pedidoViewModel));
            Assert.AreEqual(0, pedidoBienesService.Errores.Count);
        }

        #endregion

        #region Métodos Privados

        public Pedido ToPedido(PedidoViewModel pedidoViewModel)
        {
            var pedido = new Pedido
                             {
                                 Id = pedidoViewModel.Id,
                                 Solicitante = pedidoViewModel.Solicitante,
                                 Descripcion = pedidoViewModel.Descripcion,
                                 FechaCreacion = pedidoViewModel.FechaCreacion,
                                 FechaAtencion = pedidoViewModel.FechaAtencion,
                                 Estado = pedidoViewModel.Estado.GetHashCode()
                             };
            pedido.Detalles = pedidoViewModel.Detalles
                                .ConvertAll(x => new DetallePedido
                                                     {
                                                         Id = x.Id,
                                                         Pedido = pedido,
                                                         Articulo = new Articulo { Id = x.ArticuloId, Nombre = x.ArticuloNombre, CodigoCatalogo = x.ArticuloCodigoCatalogo },
                                                         CantidadSolicitada = x.CantidadSolicitada,
                                                         CantidadAtendida = x.CantidadAtendida
                                                     });

            return pedido;
        }

        #endregion
    }
}
