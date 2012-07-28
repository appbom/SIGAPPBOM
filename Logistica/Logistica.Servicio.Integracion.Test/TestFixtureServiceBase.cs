using System.Collections.Generic;
using AutoMapper;
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