using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using SIGAPPBOM.NHibernate.Repositorios;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Administracion.Parcelas;
using SIGAPPBOM.Servicio.Administracion.Parcelas;

namespace SIGAPPBOM.Servicio.Unit.Test.Parcelas
{
    [TestFixture]
    class ParcelaServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Parcela>> parcelasRepositorioFalso;
        private ParcelaService parcelaService;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            parcelasRepositorioFalso = new Mock<IRepositorio<Parcela>>();
            parcelaService = new ParcelaService(parcelasRepositorioFalso.Object, mappingEngine);
        }

        [TearDown]
        public void Dispose()
        {
            parcelasRepositorioFalso = null;
            parcelaService = null;
        }

        #endregion

        #region Tests

        [Test]
        public void TraerListaPor_NombreParcela_CUANDO_ParcelaLapiceroNoexiste_ENTONCES_DevuelveUnaListaVacía()
        {
            var nombre = "LAPICERO";
            var parcelas = new EnumerableQuery<Parcela>(new List<Parcela>());

            parcelasRepositorioFalso.Setup(x => x.TraerTodo()).Returns(parcelas);

            var parcelasViewModel = parcelaService.TraerListaPor(nombre);

            Assert.AreEqual(0, parcelasViewModel.Count);
            Assert.AreEqual("No se encontró coincidencias para el artículo", parcelaService.Errores[0]);
        }

        #endregion
    }
}
