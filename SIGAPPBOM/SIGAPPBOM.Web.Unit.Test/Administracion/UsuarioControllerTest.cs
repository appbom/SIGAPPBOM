using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Web.Areas.Administracion.Controllers;
using SIGAPPBOM.Infraestructura.Authentication;
using Moq;
using SIGAPPBOM.Servicio.Administracion.Usuarios;
using System.Web.Mvc;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Servicio.Administracion.Usuarios;
using System.Collections;

namespace SIGAPPBOM.Web.Unit.Test.Administracion
{



    [TestFixture]
    class UsuarioControllerTest
    {
        private Mock<IUsuarioService> usuarioServiceFalso;
        private Mock<IAuthenticationService> authenticationServiceFalso;
        private UsuarioController usuarioController;
        [SetUp]
        public void SetUp()
        {
            usuarioServiceFalso = new Mock<IUsuarioService>();
            authenticationServiceFalso = new Mock<IAuthenticationService>();

            var user = new UserPrincipal(new UserIdentity(1, "jperez"), new ArrayList());
            authenticationServiceFalso.Setup(x => x.ObtienerInformacionUsuario()).Returns(user);

            usuarioController = new UsuarioController(usuarioServiceFalso.Object, authenticationServiceFalso.Object);
        }
        [Test]
        [Category("Ver Pantalla Lista de Usuarios")]
        public void MostrarUsuarios_CUANDO_NavegoAAdministracionDeUsuarios_ENTONCES_MiPantallaSeDebeLLamarListaDeUsuarios()
        {
            usuarioServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<UsuarioViewModel>());

            var viewResult = (ViewResult)usuarioController.MostrarUsuario();
            var model = (List<UsuarioViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Usuarios", viewBag.Titulo);
        }
    }

    

}
