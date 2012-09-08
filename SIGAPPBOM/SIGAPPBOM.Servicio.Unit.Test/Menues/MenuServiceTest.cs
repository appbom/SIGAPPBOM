using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using SIGAPPBOM.NHibernate.Repositorios;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Administracion.Menues;
using SIGAPPBOM.Servicio.Administracion.Menues;

namespace SIGAPPBOM.Servicio.Unit.Test.Menues
{
    [TestFixture]
    class MenuServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Menu>> menuesRepositorioFalso;
        private MenuService menuService;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            menuesRepositorioFalso = new Mock<IRepositorio<Menu>>();
            menuService = new MenuService(menuesRepositorioFalso.Object, mappingEngine);
        }

        [TearDown]
        public void Dispose()
        {
            menuesRepositorioFalso = null;
            menuService = null;
        }

        #endregion

        #region Tests

        [Test]
        public void TraerListaPor_TituloMenu_CUANDO_MenuLapiceroNoexiste_ENTONCES_DevuelveUnaListaVacía()
        {
            var nombre = "LAPICERO";
            var menues = new EnumerableQuery<Menu>(new List<Menu>());

            menuesRepositorioFalso.Setup(x => x.TraerTodo()).Returns(menues);

            var menuesViewModel = menuService.TraerListaPor(nombre);

            Assert.AreEqual(0, menuesViewModel.Count);
            Assert.AreEqual("No se encontró coincidencias para el Menu", menuService.Errores[0]);
        }

        #endregion
    }
}
