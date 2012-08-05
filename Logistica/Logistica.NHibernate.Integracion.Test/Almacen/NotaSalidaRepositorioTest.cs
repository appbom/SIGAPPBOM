using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Logistica.Dominio.Almacen;
using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.Dominio.Pedidos;
using SIGAPPBOM.Logistica.Infraestructura.UnitOfWork;
using SIGAPPBOM.Logistica.NHibernate.Repositorios;
using SIGAPPBOM.Logistica.Servicio;
using SIGAPPBOM.Logistica.Servicio.Comun;
using SIGAPPBOM.Logistica.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.Logistica.NHibernate.Integracion.Test.Almacen
{
    [TestFixture]
    public class NotaSalidaRepositorioTest
    {
        UnitOfWork unitOfWork;
        IRepositorio<Articulo> articuloRepositorio;
        IRepositorio<Pedido> pedidoRepositorio;
        IRepositorio<NotaSalida> notaSalidaRepositorio;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            ServicioNHibernate.Start();
            DependencyConfigurator.Start();
            unitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            unitOfWork.Begin();
            articuloRepositorio = new Repositorio<Articulo>(unitOfWork);
            pedidoRepositorio = new Repositorio<Pedido>(unitOfWork);
            notaSalidaRepositorio = new Repositorio<NotaSalida>(unitOfWork);
        }

        [TearDown]
        public void TearDown()
        {
            unitOfWork.End();
        }
        #endregion


        #region Tests

        [Test]
        public void Grabar_GrabarUnaNotaDeSalida_Cuando_PedidoConArticulo381CantidadSolicitada10CantidadDespachada8_Entonces_SeGrabaLaNotaDeSalidaEnLaBaseDeDatos()
        {
            var articulo = new Articulo
            {
                Id = 0,
                CodigoCatalogo = "COD001",
                Nombre = "Caja 13 Kilos",
                Stock = 100
            };
            articuloRepositorio.Guardar(articulo);

            var pedido = new Pedido
            {
                Descripcion = "Test Integracion",
                Estado = Estado.PENDIENTE.GetHashCode(),
                FechaCreacion = DateTime.Parse("14/06/2012"),
                Solicitante = "Test"
            };

            var detalle = new DetallePedido
            {
                Articulo = articulo,
                CantidadSolicitada = 10,
                CantidadAtendida = 0
            };
            pedido.RegistrarDetalle(detalle);

            pedidoRepositorio.Guardar(pedido);

            var notaSalida = new NotaSalida();
            notaSalida.Pedido = pedido;
            notaSalida.Fecha = DateTime.Parse("15/06/2012");
            notaSalida.Observacion = "No tiene";
            notaSalida.Usuario = "jperez";
            notaSalida.RegistrarDetalle(new DetalleNotaSalida
            {
                Articulo = articuloRepositorio.BuscarPor(articulo.Id),
                Cantidad = 10
            });
            notaSalidaRepositorio.Guardar(notaSalida);

            var notaSalidaBD = notaSalidaRepositorio.BuscarPor(notaSalida.Id);
            Assert.AreEqual("Test Integracion", notaSalidaBD.Pedido.Descripcion);
            Assert.AreEqual("No tiene", notaSalidaBD.Observacion);
            Assert.AreEqual(1, notaSalidaBD.Detalles.Count);
            Assert.AreEqual(articulo.Id, notaSalidaBD.Detalles[0].Articulo.Id);

            notaSalidaRepositorio.Eliminar(notaSalidaBD);
            pedidoRepositorio.Eliminar(pedido);
            articuloRepositorio.Eliminar(articulo);

        }
        #endregion
    }
}
