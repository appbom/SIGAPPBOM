using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using SIGAPPBOM.NHibernate.Repositorios;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Administracion.Empacadoras;
using SIGAPPBOM.Servicio.Administracion.Empacadoras;

namespace SIGAPPBOM.Servicio.Unit.Test.Empacadoras
{
    [TestFixture]
    class EmpacadoraServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Empacadora>> empacadorasRepositorioFalso;
        private EmpacadoraService empacadoraService;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            empacadorasRepositorioFalso = new Mock<IRepositorio<Empacadora>>();
            empacadoraService = new EmpacadoraService(empacadorasRepositorioFalso.Object, mappingEngine);
        }

        [TearDown]
        public void Dispose()
        {
            empacadorasRepositorioFalso = null;
            empacadoraService = null;
        }

        #endregion

        #region Tests

        [Test]
        public void TraerListaPor_NombreEmpacadora_CUANDO_EmpacadoraLapiceroNoexiste_ENTONCES_DevuelveUnaListaVacía()
        {
            var nombre = "LAPICERO";
            var empacadoras = new EnumerableQuery<Empacadora>(new List<Empacadora>());

            empacadorasRepositorioFalso.Setup(x => x.TraerTodo()).Returns(empacadoras);

            var articulosViewModel = empacadoraService.TraerListaPor(nombre);

            Assert.AreEqual(0, articulosViewModel.Count);
            Assert.AreEqual("No se encontró coincidencias para el artículo", empacadoraService.Errores[0]);
        }

        #endregion
    }
}
