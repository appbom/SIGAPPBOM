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

namespace SIGAPPBOM.Logistica.Servicio.Unit.Test.Pedidos
{
    [TestFixture]
    public class PedidoBienesServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Pedido>> pedidosRepositorio;
        private Mock<IRepositorio<Articulo>> articulosRepositorio;
        private PedidoBienesService pedidoBienesService;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            pedidosRepositorio = new Mock<IRepositorio<Pedido>>();
            articulosRepositorio = new Mock<IRepositorio<Articulo>>();
            pedidoBienesService = new PedidoBienesService(pedidosRepositorio.Object, articulosRepositorio.Object, mappingEngine);
        }

        [TearDown]
        public void Dispose()
        {
            pedidosRepositorio = null;
            articulosRepositorio = null;
            pedidoBienesService = null;
        }

        #endregion

        #region Tests

        [Test]
        public void TraerLista_CUANDO_NoExistenPedidosDeBienes_ENTONCES_DevuelveUnaListaVacía()
        {
            var pedidos = new EnumerableQuery<Pedido>(new List<Pedido>());

            pedidosRepositorio.Setup(x => x.TraerTodo()).Returns(pedidos);

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

            pedidosRepositorio.Setup(x => x.TraerTodo()).Returns(pedidos);

            var pedidosViewModel = pedidoBienesService.TraerLista();

            Assert.AreEqual(2, pedidosViewModel.Count);
        }

        #endregion
    }
}
