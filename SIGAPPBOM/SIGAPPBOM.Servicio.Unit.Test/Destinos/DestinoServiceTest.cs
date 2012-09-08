using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using SIGAPPBOM.NHibernate.Repositorios;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Administracion.Destinos;
using SIGAPPBOM.Servicio.Administracion.Destinos;

namespace SIGAPPBOM.Servicio.Unit.Test.Destinos
{
    [TestFixture]
    class DestinoServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Destino>> destinosRepositorioFalso;
        private DestinoService destinoService;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            destinosRepositorioFalso = new Mock<IRepositorio<Destino>>();
            destinoService = new DestinoService(destinosRepositorioFalso.Object, mappingEngine);
        }

        [TearDown]
        public void Dispose()
        {
            destinosRepositorioFalso = null;
            destinoService = null;
        }

        #endregion

        #region Tests

        [Test]
        public void TraerListaPor_NombreDestino_CUANDO_DestinoLapiceroNoexiste_ENTONCES_DevuelveUnaListaVacía()
        {
            var nombre = "LAPICERO";
            var destinos = new EnumerableQuery<Destino>(new List<Destino>());

            destinosRepositorioFalso.Setup(x => x.TraerTodo()).Returns(destinos);

            var destinosViewModel = destinoService.TraerListaPor(nombre);

            Assert.AreEqual(0, destinosViewModel.Count);
            Assert.AreEqual("No se encontró coincidencias para el artículo", destinoService.Errores[0]);
        }

        #endregion
    }
}
