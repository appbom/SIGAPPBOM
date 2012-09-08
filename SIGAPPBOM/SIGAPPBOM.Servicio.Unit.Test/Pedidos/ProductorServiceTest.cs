using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using SIGAPPBOM.NHibernate.Repositorios;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Administracion.Productores;
using SIGAPPBOM.Servicio.Logistica.Productores;

namespace SIGAPPBOM.Servicio.Unit.Test.Pedidos
{
    [TestFixture]
    class ProductorServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Productor>> productoresRepositorioFalso;
        private ProductorService productorService;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            productoresRepositorioFalso = new Mock<IRepositorio<Productor>>();
            productorService = new ProductorService(productoresRepositorioFalso.Object, mappingEngine);
        }

        [TearDown]
        public void Dispose()
        {
            productoresRepositorioFalso = null;
            productorService = null;
        }

        #endregion

        #region Tests

        [Test]
        public void TraerListaPor_NombreArticulo_CUANDO_ArticuloLapiceroNoexiste_ENTONCES_DevuelveUnaListaVacía()
        {
            var nombre = "LAPICERO";
            var productores = new EnumerableQuery<Productor>(new List<Productor>());

            productoresRepositorioFalso.Setup(x => x.TraerTodo()).Returns(productores);

            var articulosViewModel = productorService.TraerListaPor(nombre);

            Assert.AreEqual(0, articulosViewModel.Count);
            Assert.AreEqual("No se encontró coincidencias para el artículo", productorService.Errores[0]);
        }

        #endregion
    }
}
