using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.NHibernate.Repositorios;
using Moq;
using SIGAPPBOM.Logistica.Servicio.Articulos;

namespace SIGAPPBOM.Logistica.Servicio.Unit.Test.Articulos
{
    [TestFixture]
    public class ArticuloServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Articulo>> articulosRepositorioFalso;
        private ArticuloService articuloService;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            articulosRepositorioFalso = new Mock<IRepositorio<Articulo>>();
            articuloService = new ArticuloService(articulosRepositorioFalso.Object, mappingEngine);
        }

        [TearDown]
        public void Dispose()
        {
            articulosRepositorioFalso = null;
            articuloService = null;
        }

        #endregion

        #region Tests

        [Test]
        public void TraerListaPor_NombreArticulo_CUANDO_ArticuloLapiceroNoexiste_ENTONCES_DevuelveUnaListaVacía()
        {
            var nombre = "LAPICERO";
            var articulos = new EnumerableQuery<Articulo>(new List<Articulo>());

            articulosRepositorioFalso.Setup(x => x.TraerTodo()).Returns(articulos);

            var articulosViewModel = articuloService.TraerListaPor(nombre);

            Assert.AreEqual(0, articulosViewModel.Count);
            Assert.AreEqual("No se encontró coincidencias para el artículo", articuloService.Errores[0]);
        }

        [Test]
        public void TraerListaPor_NombreArticulo_CUANDO_ArticuloLapiceroExisteYTiene2Coincidencias_ENTONCES_DevuelveUnaListaCon2Coincidencias()
        {
            var nombre = "LAPICERO";
            var articulos = new EnumerableQuery<Articulo>(new List<Articulo>
                                                          {
                                                              new Articulo{Id = 1,Nombre = "LAPICERO PILOT TINTA LIQUIDA",Stock = 100, CodigoCatalogo= ""},
                                                              new Articulo{Id = 2,Nombre = "LAPICERO FABER PUNTA FINA",Stock = 50, CodigoCatalogo= ""},
                                                          });

            articulosRepositorioFalso.Setup(x => x.TraerTodo()).Returns(articulos);

            var articulosViewModel = articuloService.TraerListaPor(nombre);

            Assert.AreEqual(2, articulosViewModel.Count);
        }

        #endregion
    }
}
