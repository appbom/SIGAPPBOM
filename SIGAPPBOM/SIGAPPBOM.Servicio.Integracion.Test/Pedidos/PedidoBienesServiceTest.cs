using System;
using System.Linq;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Logistica.Articulos;
using SIGAPPBOM.Dominio.Logistica.Pedidos;
using SIGAPPBOM.Servicio.Comun;
using SIGAPPBOM.Servicio.Logistica.Pedidos;
using SIGAPPBOM.Servicio.ViewModels;
using StructureMap;

namespace SIGAPPBOM.Servicio.Integracion.Test.Pedidos
{
    [TestFixture]
    public class PedidoBienesServiceTest : TestFixtureServiceBase
    {
        private IPedidoInsumosService servicioPedido;

        [SetUp]
        public void Init()
        {
            servicioPedido = ObjectFactory.GetInstance<IPedidoInsumosService>();
            UnitOfWork.Begin();
        }

        [TearDown]
        public void Dispose()
        {
            if (Errores.Count == 0)
                UnitOfWork.End();
        }

        #region Traer Lista

        [Test]
        public void TraerLista_CUANDO_NohayPedidosRegistrados_ENTONCES_DebeDevolverUnaListaVacía()
        {
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

        #endregion

        #region Traer Por

        [Test]
        public void TraerPor_CUANDO_PedidoEsValido_ENTONCES_DebeDevolverPedidoCorrectamente()
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

            var pedidoViewModel = servicioPedido.TraerPor(pedido1.Id);

            Assert.NotNull(pedidoViewModel);
            Assert.AreEqual("Test Integracion 1", pedidoViewModel.Descripcion);

            foreach (string msg in servicioPedido.Errores)
                System.Diagnostics.Debug.WriteLine(msg);
            Errores = servicioPedido.Errores;

            pedidoRepositorio.Eliminar(pedido1);
            articuloRepositorio.Eliminar(articulo1);
            articuloRepositorio.Eliminar(articulo2);
        }

        #endregion

        #region Grabar

        [Test]
        public void Grabar_CuandoPedidoEsValido_DebeGrabarEnBaseDeDatos()
        {
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
                Solicitante = "Solicitante Test"
            };

            var detalle = new DetallePedido
            {
                Articulo = articulo1,
                CantidadSolicitada = 10,
                CantidadAtendida = 0
            };
            pedido1.RegistrarDetalle(detalle);
            detalle = new DetallePedido
            {
                Articulo = articulo1,
                CantidadSolicitada = 10,
                CantidadAtendida = 0
            };
            pedido1.RegistrarDetalle(detalle);

            var pedido = MappingEngine.Map<Pedido, PedidoViewModel>(pedido1);
            servicioPedido.Grabar(pedido);

            var pedidoResult = pedidoRepositorio.BuscarPor(pedido.Id);
            Assert.IsNotNull(pedidoResult);

            pedidoRepositorio.Eliminar(pedidoResult);
            articuloRepositorio.Eliminar(articulo1);
            articuloRepositorio.Eliminar(articulo2);
        }

        #endregion

        #region Actualizar

        [Test]
        public void Editar_CuandoPedidoTiene2DetallesElimino1YEdito1_DebeGrabarEnBaseDeDatos()
        {
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
                Nombre = "Caja 15 Kilos",
                Stock = 100
            };
            articuloRepositorio.Guardar(articulo2);

            //Pedidos

            var pedido = new Pedido
            {
                Descripcion = "Test Integracion",
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
            var detalle2 = new DetallePedido
            {
                Articulo = articulo2,
                CantidadSolicitada = 20,
                CantidadAtendida = 0
            };
            pedido.RegistrarDetalle(detalle1);
            pedido.RegistrarDetalle(detalle2);

            pedidoRepositorio.Guardar(pedido);

            var pedidoResult = pedidoRepositorio.BuscarPor(pedido.Id);
            var pedidoViewModel = MappingEngine.Map<Pedido, PedidoViewModel>(pedidoResult);

            pedidoViewModel.Descripcion = "Test Edición Integracion";
            pedidoViewModel.Detalles.Clear();
            pedidoViewModel.Detalles.Add(
                new DetallePedidoViewModel
                {
                    ArticuloId = articulo2.Id,
                    ArticuloNombre = articulo2.Nombre,
                    CantidadSolicitada = 40,
                    Item = 1
                });

            var result = servicioPedido.Actualizar(pedidoViewModel);
            Assert.IsTrue(result);

            pedidoResult = pedidoRepositorio.BuscarPor(pedidoViewModel.Id);

            Assert.IsNotNull(pedidoResult);
            Assert.AreEqual("Test Edición Integracion", pedidoResult.Descripcion);
            Assert.AreEqual(1, pedidoResult.Detalles.Count);
            Assert.AreEqual(articulo2.Id, pedidoResult.Detalles[0].Articulo.Id);
            Assert.AreEqual(articulo2.Nombre, pedidoResult.Detalles[0].Articulo.Nombre);
            Assert.AreEqual(40, pedidoResult.Detalles[0].CantidadSolicitada);

            pedidoRepositorio.Eliminar(pedidoResult);
            articuloRepositorio.Eliminar(articulo1);
            articuloRepositorio.Eliminar(articulo2);
        }

        [Test]
        public void Editar_CuandoPedidoTiene1DetalleYLoEdito_DebeGrabarEnBaseDeDatos()
        {
            var articulo1 = new Articulo
            {
                Id = 0,
                CodigoCatalogo = "COD001",
                Nombre = "Caja 13 Kilos",
                Stock = 100
            };
            articuloRepositorio.Guardar(articulo1);

            //Pedidos

            var pedido = new Pedido
            {
                Descripcion = "Test Integracion",
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

            pedido.RegistrarDetalle(detalle1);

            pedidoRepositorio.Guardar(pedido);

            var pedidoResult = pedidoRepositorio.BuscarPor(pedido.Id);
            var pedidoViewModel = MappingEngine.Map<Pedido, PedidoViewModel>(pedidoResult);

            pedidoViewModel.Descripcion = "Test Edición Integracion";
            pedidoViewModel.Detalles.Clear();
            pedidoViewModel.Detalles.Add(
                new DetallePedidoViewModel
                {
                    ArticuloId = articulo1.Id,
                    ArticuloNombre = articulo1.Nombre,
                    CantidadSolicitada = 20,
                    Item = 1
                });

            var result = servicioPedido.Actualizar(pedidoViewModel);
            Assert.IsTrue(result);

            pedidoResult = pedidoRepositorio.BuscarPor(pedidoViewModel.Id);

            Assert.IsNotNull(pedidoResult);
            Assert.AreEqual("Test Edición Integracion", pedidoResult.Descripcion);
            Assert.AreEqual(1, pedidoResult.Detalles.Count);
            Assert.AreEqual(articulo1.Id, pedidoResult.Detalles[0].Articulo.Id);
            Assert.AreEqual(articulo1.Nombre, pedidoResult.Detalles[0].Articulo.Nombre);
            Assert.AreEqual(20, pedidoResult.Detalles[0].CantidadSolicitada);

            pedidoRepositorio.Eliminar(pedidoResult);
            articuloRepositorio.Eliminar(articulo1);
        }

        [Test]
        public void Editar_CuandoPedidoTiene2DetallesLosEliminoYAgrego2DetallesNuevos_DebeGrabarEnBaseDeDatos()
        {
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
                CodigoCatalogo = "COD002",
                Nombre = "Caja 15 Kilos",
                Stock = 100
            };
            articuloRepositorio.Guardar(articulo2);

            var articulo3 = new Articulo
            {
                Id = 0,
                CodigoCatalogo = "COD003",
                Nombre = "Caja 18 Kilos",
                Stock = 100
            };
            articuloRepositorio.Guardar(articulo3);

            var articulo4 = new Articulo
            {
                Id = 0,
                CodigoCatalogo = "COD004",
                Nombre = "Caja 20 Kilos",
                Stock = 100
            };
            articuloRepositorio.Guardar(articulo4);

            //Pedidos

            var pedido = new Pedido
            {
                Descripcion = "Test Integracion",
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
            var detalle2 = new DetallePedido
            {
                Articulo = articulo2,
                CantidadSolicitada = 20,
                CantidadAtendida = 0
            };
            pedido.RegistrarDetalle(detalle1);
            pedido.RegistrarDetalle(detalle2);

            pedidoRepositorio.Guardar(pedido);

            var pedidoResult = pedidoRepositorio.BuscarPor(pedido.Id);
            var pedidoViewModel = MappingEngine.Map<Pedido, PedidoViewModel>(pedidoResult);

            pedidoViewModel.Descripcion = "Test Edición Integracion";
            pedidoViewModel.Detalles.Clear();
            pedidoViewModel.Detalles.Add(
                new DetallePedidoViewModel
                {
                    ArticuloId = articulo3.Id,
                    ArticuloNombre = articulo3.Nombre,
                    CantidadSolicitada = 60,
                    Item = 1
                });
            pedidoViewModel.Detalles.Add(
                new DetallePedidoViewModel
                {
                    ArticuloId = articulo4.Id,
                    ArticuloNombre = articulo4.Nombre,
                    CantidadSolicitada = 70,
                    Item = 1
                });

            var result = servicioPedido.Actualizar(pedidoViewModel);
            Assert.IsTrue(result);

            pedidoResult = pedidoRepositorio.BuscarPor(pedidoViewModel.Id);

            Assert.IsNotNull(pedidoResult);
            Assert.AreEqual("Test Edición Integracion", pedidoResult.Descripcion);
            Assert.AreEqual(2, pedidoResult.Detalles.Count);
            Assert.IsNotNull(pedidoResult.Detalles.First(x => x.Articulo.Id == articulo3.Id));
            Assert.IsNotNull(pedidoResult.Detalles.First(x => x.Articulo.Id == articulo4.Id));

            pedidoRepositorio.Eliminar(pedidoResult);
            articuloRepositorio.Eliminar(articulo1);
            articuloRepositorio.Eliminar(articulo2);
            articuloRepositorio.Eliminar(articulo3);
            articuloRepositorio.Eliminar(articulo4);
        }

        #endregion

        #region Eliminar

        [Test]
        public void Eliminar_CUANDO_PedidoEsValido_ENTONCES_DebeEliminarPedidoCorrectamente()
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

            Assert.IsTrue(servicioPedido.Eliminar(pedido1.Id));
            Assert.Null(servicioPedido.TraerPor(pedido1.Id));

            foreach (string msg in servicioPedido.Errores)
                System.Diagnostics.Debug.WriteLine(msg);
            Errores = servicioPedido.Errores;

            pedidoRepositorio.Eliminar(pedido1);
            articuloRepositorio.Eliminar(articulo1);
            articuloRepositorio.Eliminar(articulo2);
        }

        #endregion

    }
}
