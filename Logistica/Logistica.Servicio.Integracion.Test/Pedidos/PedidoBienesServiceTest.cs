using System;
using NUnit.Framework;
using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.Dominio.Pedidos;
using SIGAPPBOM.Logistica.Servicio.Comun;
using SIGAPPBOM.Logistica.Servicio.Pedidos;
using StructureMap;

namespace SIGAPPBOM.Logistica.Servicio.Integracion.Test.Pedidos
{
    [TestFixture]
    public class PedidoBienesServiceTest : TestFixtureServiceBase
    {

        [Test]
        public void TraerLista_CUANDO_NohayPedidosRegistrados_ENTONCES_DebeDevolverUnaListaVacía()
        {
            var servicioPedido = ObjectFactory.GetInstance<IPedidoBienesService>();
            var pedidosViewModel = servicioPedido.TraerLista();
            Assert.AreEqual(0, pedidosViewModel.Count);
        }

        [Test]
        public void TraerLista_CUANDO_Hay2PedidosRegistrados_ENTONCES_DebeDevolverUnaListaCon2Pedidos()
        {
            //Articulos
            var articulo1 = new Articulo
            {
                Id = 0,
                CodigoCatalogo = "COD001",
                Nombre = "Caja 13 Kilos",
                Stock = 100
            };
            articuloRepositorio.Guardar(articulo1);

            var articulo2 = new Articulo
            {
                Id = 0,
                CodigoCatalogo = "COD001",
                Nombre = "Caja 13 Kilos",
                Stock = 100
            };
            articuloRepositorio.Guardar(articulo2);

            //Pedidos
            var pedido1 = new Pedido
            {
                Descripcion = "Test Integracion 1",
                Estado = Estado.PENDIENTE.GetHashCode(),
                FechaCreacion = DateTime.Parse("14/06/2012"),
                Solicitante = "Test"
            };

            var detalle1 = new DetallePedido
            {
                Articulo = articulo1,
                CantidadSolicitada = 10,
                CantidadAtendida = 0
            };
            pedido1.RegistrarDetalle(detalle1);

            pedidoRepositorio.Guardar(pedido1);

            var pedido2 = new Pedido
            {
                Descripcion = "Test Integracion 2",
                Estado = Estado.PENDIENTE.GetHashCode(),
                FechaCreacion = DateTime.Parse("14/06/2012"),
                Solicitante = "Test"
            };

            var detalle2 = new DetallePedido
            {
                Articulo = articulo2,
                CantidadSolicitada = 10,
                CantidadAtendida = 0
            };
            pedido1.RegistrarDetalle(detalle2);

            pedidoRepositorio.Guardar(pedido2);

            var servicioPedido = ObjectFactory.GetInstance<IPedidoBienesService>();
            var pedidosViewModel = servicioPedido.TraerLista();

            Assert.AreEqual(2, pedidosViewModel.Count);
            Assert.AreEqual("Test Integracion 1", pedidosViewModel[0].Descripcion);
            Assert.AreEqual("Test Integracion 2", pedidosViewModel[1].Descripcion);

            foreach (string msg in servicioPedido.Errores)
                System.Diagnostics.Debug.WriteLine(msg);
            Errores = servicioPedido.Errores;

            pedidoRepositorio.Eliminar(pedido1);
            pedidoRepositorio.Eliminar(pedido2);
            articuloRepositorio.Eliminar(articulo1);
            articuloRepositorio.Eliminar(articulo2);

        }
    }
}
