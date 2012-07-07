using System.Collections.Generic;
using NUnit.Framework;
using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.Dominio.Pedidos;
using SIGAPPBOM.Logistica.Infraestructura.UnitOfWork;
using SIGAPPBOM.Logistica.NHibernate.Repositorios;
using SIGAPPBOM.Logistica.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.Logistica.Servicio.Integracion.Test
{
    [TestFixture]
    public class TestFixtureServiceBase
    {
        protected IUnitOfWork UnitOfWork;
        protected IRepositorio<Articulo> articuloRepositorio;
        protected IRepositorio<Pedido> pedidoRepositorio;
        protected IList<string> Errores;

        [TestFixtureSetUp]
        public void SetupFixture()
        {
            Errores = new List<string>();
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
            articuloRepositorio = new Repositorio<Articulo>(UnitOfWork);
            pedidoRepositorio = new Repositorio<Pedido>(UnitOfWork);
        }

        [SetUp]
        public void Init()
        {
            UnitOfWork.Begin();
        }

        [TearDown]
        public void Dispose()
        {
            if (Errores.Count == 0)
                UnitOfWork.End();
        }
    }
}