using NUnit.Framework;
using SIGAPPBOM.Dominio.Logistica.Articulos;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.NHibernate.Integracion.Test.Articulos
{

    [TestFixture]
    public class ArticuloRepositorioTest
    {
        private UnitOfWork UnitOfWork;
        private IRepositorio<Articulo> articuloRepositorio;

        [SetUp]
        public void Setup()
        {
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            UnitOfWork.Begin();
            articuloRepositorio = new Repositorio<Articulo>(UnitOfWork);
        }

        [Test]
        public void Grabar_Cuando_GraboUnArticulo_Entonces_SeGrabaraEnLaBaseDeDatos()
        {
            var articulo = new Articulo
                               {
                                   Id = 0,
                                   CodigoCatalogo = "COD001",
                                   Nombre = "Caja 13 Kilos",
                                   Stock = 100
                               };

            articuloRepositorio.Guardar(articulo);

            Articulo articuloGuardado = articuloRepositorio.BuscarPor(articulo.Id);

            Assert.AreEqual("COD001", articuloGuardado.CodigoCatalogo);
            Assert.AreEqual("Caja 13 Kilos", articuloGuardado.Nombre);
            Assert.AreEqual(100, articuloGuardado.Stock);

            articuloRepositorio.Eliminar(articuloGuardado);
        }


        [TearDown]
        public void TearDown()
        {
            UnitOfWork.End();
        }
    }
}

