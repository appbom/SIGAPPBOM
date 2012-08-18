using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Logistica.Articulos;
using SIGAPPBOM.Dominio.Logistica.Pedidos;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio.Comun;
using SIGAPPBOM.Servicio.Logistica.Pedidos;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Unit.Test.Pedidos
{
    [TestFixture]
    public class PedidoBienesServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Pedido>> pedidosRepositorioFalso;
        private Mock<IRepositorio<Articulo>> articulosRepositorioFalso;
        private PedidoInsumosService pedidoInsumosService;
        private PedidoViewModel pedidoViewModel;
        private Pedido pedidoModelo;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            pedidosRepositorioFalso = new Mock<IRepositorio<Pedido>>();
            articulosRepositorioFalso = new Mock<IRepositorio<Articulo>>();
            pedidoInsumosService = new PedidoInsumosService(pedidosRepositorioFalso.Object, articulosRepositorioFalso.Object, mappingEngine);
            pedidoViewModel = new PedidoViewModel { Id = 1, Descripcion = "Pedido 01", Solicitante = "Pedro Dominguez", Estado = Estado.PENDIENTE, FechaCreacion = DateTime.Parse("12/05/2012") };
            pedidoModelo = new Pedido { Id = 1, Descripcion = "Pedido 01", Solicitante = "Pedro Dominguez", Estado = Estado.PENDIENTE.GetHashCode(), FechaCreacion = DateTime.Parse("12/05/2012") };
        }

        [TearDown]
        public void Dispose()
        {
            pedidosRepositorioFalso = null;
            articulosRepositorioFalso = null;
            pedidoInsumosService = null;
        }

        #endregion

        #region TraerLista

        [Test]
        public void TraerLista_CUANDO_NoExistenPedidosDeBienes_ENTONCES_DevuelveUnaListaVacía()
        {
            var pedidos = new EnumerableQuery<Pedido>(new List<Pedido>());

            pedidosRepositorioFalso.Setup(x => x.TraerTodo()).Returns(pedidos);

            var pedidosViewModel = pedidoInsumosService.TraerLista();

            Assert.AreEqual(0, pedidosViewModel.Count);
            Assert.AreEqual("No hay pedidos de bienes registrados", pedidoInsumosService.Errores[0]);
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

            var pedidosViewModel = pedidoInsumosService.TraerLista();

            Assert.AreEqual(2, pedidosViewModel.Count);
        }

        #endregion

        #region Traer Por

        [Test]
        public void TraerPor_CUANDO_PedidoNoExiste_ENTONCES_DevuelveNull()
        {
            pedidoViewModel = pedidoInsumosService.TraerPor(-1);
            Assert.IsNull(pedidoViewModel);
        }

        [Test]
        public void TraerPedido_Cuando_PedidoConItemDeDetalleLapicero_Entonces_DevolverPedidoViewModelCorrectamente()
        {
            pedidoViewModel.Estado = Estado.PENDIENTE;
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            var pedidoSalida = pedidoInsumosService.TraerPor(pedidoViewModel.Id);
            Assert.IsNotNull(pedidoSalida);
        }
        
        #endregion

        #region Grabar

        [Test]
        public void Grabar_Cuando_SolcitanteEsVacio_Entonces_RetornarFalsoYMensajeError_IngresarSolicitanteParaPedido()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Solicitante = "";
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel());

            Assert.IsFalse(pedidoInsumosService.Grabar(pedidoViewModel));
            Assert.AreEqual("Ingresar solicitante para pedido", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Grabar_Cuando_DescripcionDePedidoVacia_Entonces_RetornarFalsoYMensajeError_IngresarDescripcionDePedido()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Solicitante = "Pedro Perez";
            pedidoViewModel.Descripcion = "";
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel());

            Assert.IsFalse(pedidoInsumosService.Grabar(pedidoViewModel));
            Assert.AreEqual("Ingresar descripción de pedido", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Grabar_Cuando_DetalleDePedidoEstaVacio_Entonces_RetornarFalsoYMensajeError_IngresarDetalleDePedido()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Solicitante = "Pedro Perez";
            Assert.IsFalse(pedidoInsumosService.Grabar(pedidoViewModel));
            Assert.AreEqual("Añadir artículos al detalle de pedido", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Grabar_Cuando_CantidadPedidaEsMenorACero_Entonces_RetornarFalsoYMensajeError_IngresarCantidadMayorACero()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Solicitante = "Pedro Perez";
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 101, ArticuloNombre = "Articulo", CantidadSolicitada = 0 });

            Assert.IsFalse(pedidoInsumosService.Grabar(pedidoViewModel));
            Assert.AreEqual("Item 1: Ingresar cantidad mayor a cero", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Grabar_Cuando_ArticuloNoExiste_Entonces_RetornarFalsoYMensajeError_ArticuloNoExiste()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1000, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            int articuloId = 1;
            Articulo articulo = null;
            articulosRepositorioFalso.Setup(s => s.BuscarPor(articuloId)).Returns(articulo);

            Assert.IsFalse(pedidoInsumosService.Grabar(pedidoViewModel));
            Assert.AreEqual("Item 1: Articulo no existe", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Grabar_Cuando_NoExistanErrores_Entonces_RetornarTrueYCantidadErroresCero()
        {
            pedidoViewModel.Id = 0;
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            Articulo articulo = new Articulo { Id = 1 };
            articulosRepositorioFalso.Setup(s => s.BuscarPor(articulo.Id)).Returns(articulo);

            Assert.IsTrue(pedidoInsumosService.Grabar(pedidoViewModel));
            Assert.AreEqual(0, pedidoInsumosService.Errores.Count);
        }

        #endregion

        #region Actualizar

        [Test]
        public void Actualizar_Cuando_PedidoNoExiste_Entonces_RetornarFalsoYMensajeError_PedidoNoExiste()
        {
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(101)).Returns((Pedido)null);

            pedidoViewModel.Solicitante = string.Empty;

            Assert.IsFalse(pedidoInsumosService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Pedido no existe", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Actualizar_Cuando_SolcitanteEsVacio_Entonces_RetornarFalsoYMensajeError_IngresarSolicitanteParaPedido()
        {
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            pedidoViewModel.Solicitante = string.Empty;

            Assert.IsFalse(pedidoInsumosService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Ingresar solicitante para pedido", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Actualizar_Cuando_DescripcionDePedidoVacia_Entonces_RetornarFalsoYMensajeError_IngresarDescripcionDePedido()
        {
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            pedidoViewModel.Descripcion = "";

            Assert.IsFalse(pedidoInsumosService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Ingresar descripción de pedido", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Actualizar_Cuando_DetalleDePedidoEstaVacio_Entonces_RetornarFalsoYMensajeError_IngresarDetalleDePedido()
        {
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            Assert.IsFalse(pedidoInsumosService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Añadir artículos al detalle de pedido", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Actualizar_Cuando_CantidadPedidaEsMenorACero_Entonces_RetornarFalsoYMensajeError_IngresarCantidadMayorACero()
        {
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            var pedido = ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            pedidoViewModel.Detalles.Clear();
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 101, ArticuloNombre = "Articulo", CantidadSolicitada = 0 });

            Assert.IsFalse(pedidoInsumosService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Item 1: Ingresar cantidad mayor a cero", pedidoInsumosService.Errores[0]);
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

            Assert.IsFalse(pedidoInsumosService.Actualizar(pedidoViewModel));
            Assert.AreEqual("Item 1: Articulo no existe", pedidoInsumosService.Errores[0]);
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

            Assert.IsTrue(pedidoInsumosService.Actualizar(pedidoViewModel));
            Assert.AreEqual(0, pedidoInsumosService.Errores.Count);
        }

        #endregion

        #region Eliminar

        [Test]
        public void Eliminar_EliminacionDePedidos_Cuando_SeQuiereEliminarUnPedidoInexistente_Entonces_RetornaFalsoYMensajeDeError_PedidoNoExiste()
        {
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns((Pedido)null);

            Assert.IsFalse(pedidoInsumosService.Eliminar(pedidoViewModel.Id));
            Assert.AreEqual("No se eliminó - Pedido no existe", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Eliminar_EliminacionDePedidos_Cuando_SeQuiereEliminarUnPedidoConEstadoAtendido_Entonces_RetornaFalsoYMensajeDeError_EstadoDePedidoEsIncorrecto()
        {
            pedidoViewModel.Estado = Estado.ATENDIDO;
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            Pedido pedido = this.ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            Assert.IsFalse(pedidoInsumosService.Eliminar(pedidoViewModel.Id));
            Assert.AreEqual("No se eliminó - Estado del pedido es incorrecto", pedidoInsumosService.Errores[0]);
        }

        [Test]
        public void Eliminar_EliminacionDePedidos_Cuando_SeQuiereEliminarUnPedidoYNoExistanErrores_Entonces_RetornarTrueYCantidadErroresCero()
        {
            pedidoViewModel.Estado = Estado.PENDIENTE;
            pedidoViewModel.Detalles.Add(new DetallePedidoViewModel { Item = 1, ArticuloId = 1, ArticuloNombre = "Articulo", CantidadSolicitada = 10 });
            Pedido pedido = this.ToPedido(pedidoViewModel);
            pedidosRepositorioFalso.Setup(x => x.BuscarPor(pedidoViewModel.Id)).Returns(pedido);

            Assert.IsTrue(pedidoInsumosService.Eliminar(pedidoViewModel.Id));
            Assert.AreEqual(0, pedidoInsumosService.Errores.Count);
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
