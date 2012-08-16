using System;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Articulos;
using SIGAPPBOM.Dominio.Pedidos;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio;
using SIGAPPBOM.Servicio.Comun;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.NHibernate.Integracion.Test.Pedidos
{

    [TestFixture]
    public class PedidoRepositorioTest
    {
        private UnitOfWork unitOfWork;
        private IRepositorio<Articulo> articuloRepositorio;
        private IRepositorio<Pedido> pedidoRepositorio;

        [SetUp]
        public void Setup()
        {
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            unitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            unitOfWork.Begin();
            articuloRepositorio = new Repositorio<Articulo>(unitOfWork);
            pedidoRepositorio = new Repositorio<Pedido>(unitOfWork);
        }

        [Test]
        public void Grabar_Cuando_GraboUnPedido_Entonces_SeGrabaraEnLaBaseDeDatos()
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

            var pedidoGuardado = pedidoRepositorio.BuscarPor(pedido.Id);

            Assert.AreEqual("Test Integracion", pedidoGuardado.Descripcion);
            Assert.AreEqual(Estado.PENDIENTE.GetHashCode(), pedidoGuardado.Estado);
            Assert.AreEqual(DateTime.Parse("14/06/2012"), pedidoGuardado.FechaCreacion);
            Assert.AreEqual("Test", pedidoGuardado.Solicitante);
            Assert.AreEqual(1, pedidoGuardado.Detalles.Count);
            Assert.AreEqual(articulo.Id, pedidoGuardado.Detalles[0].Articulo.Id);

            pedidoRepositorio.Eliminar(pedidoGuardado);
            articuloRepositorio.Eliminar(articulo);
        }

        [TearDown]
        public void TearDown()
        {
            unitOfWork.End();
        }
    }
}