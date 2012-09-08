using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using SIGAPPBOM.NHibernate.Repositorios;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Administracion.Usuarios;
using SIGAPPBOM.Servicio.Administracion.Usuarios;

namespace SIGAPPBOM.Servicio.Unit.Test.Usuarios
{
    [TestFixture]
    class UsuarioServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<Usuario>> usuariosRepositorioFalso;
        private UsuarioService usuarioService;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            usuariosRepositorioFalso = new Mock<IRepositorio<Usuario>>();
            usuarioService = new UsuarioService(usuariosRepositorioFalso.Object, mappingEngine);
        }

        [TearDown]
        public void Dispose()
        {
            usuariosRepositorioFalso = null;
            usuarioService = null;
        }

        #endregion

        #region Tests

        [Test]
        public void TraerListaPor_NombreUsuario_CUANDO_UsuarioLapiceroNoexiste_ENTONCES_DevuelveUnaListaVacía()
        {
            var nombre = "LAPICERO";
            var usuarios = new EnumerableQuery<Usuario>(new List<Usuario>());

            usuariosRepositorioFalso.Setup(x => x.TraerTodo()).Returns(usuarios);

            var usuariosViewModel = usuarioService.TraerListaPor(nombre);

            Assert.AreEqual(0, usuariosViewModel.Count);
            Assert.AreEqual("No se encontró coincidencias para el Usuario", usuarioService.Errores[0]);
        }

        #endregion
    }
}
