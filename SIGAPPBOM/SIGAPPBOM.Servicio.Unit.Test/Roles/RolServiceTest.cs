using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using SIGAPPBOM.NHibernate.Repositorios;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Administracion.Roles;
using SIGAPPBOM.Servicio.Administracion.Roles;

namespace SIGAPPBOM.Servicio.Unit.Test.Roles
{
    [TestFixture]
    class RolServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Rol>> rolesRepositorioFalso;
        private RolService rolService;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            rolesRepositorioFalso = new Mock<IRepositorio<Rol>>();
            rolService = new RolService(rolesRepositorioFalso.Object, mappingEngine);
        }

        [TearDown]
        public void Dispose()
        {
            rolesRepositorioFalso = null;
            rolService = null;
        }

        #endregion

        #region Tests

        [Test]
        public void TraerListaPor_NombreRol_CUANDO_RolLapiceroNoexiste_ENTONCES_DevuelveUnaListaVacía()
        {
            var id = 999;
            var roles = new EnumerableQuery<Rol>(new List<Rol>());

            rolesRepositorioFalso.Setup(x => x.TraerTodo()).Returns(roles);

            var rolesViewModel = rolService.TraerListaPor(id);

            Assert.AreEqual(0, rolesViewModel.Count);
            Assert.AreEqual("No se encontró coincidencias para el Rol", rolService.Errores[0]);
        }

        #endregion
    }
}
