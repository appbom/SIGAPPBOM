using System.Collections.Generic;
using AutoMapper;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Logistica.Articulos;
using SIGAPPBOM.Dominio.Logistica.Pedidos;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.Servicio.Integracion.Test
{
    [TestFixture]
    public class TestFixtureServiceBase
    {
        protected IUnitOfWork UnitOfWork;
        protected IRepositorio<Articulo> articuloRepositorio;
        protected IRepositorio<Pedido> pedidoRepositorio;
        protected IList<string> Errores;
        protected IMappingEngine MappingEngine;

        [TestFixtureSetUp]
        public void SetupFixture()
        {
            Errores = new List<string>();
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = ObjectFactory.GetInstance<IUnitOfWork>();
            MappingEngine = ObjectFactory.GetInstance<IMappingEngine>();
            articuloRepositorio = new Repositorio<Articulo>(UnitOfWork);
            pedidoRepositorio = new Repositorio<Pedido>(UnitOfWork);

        }
    }
}