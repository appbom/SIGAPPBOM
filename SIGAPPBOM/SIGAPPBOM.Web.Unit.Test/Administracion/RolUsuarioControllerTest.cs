using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Web.Areas.Administracion.Controllers;
using SIGAPPBOM.Infraestructura.Authentication;
using Moq;
using SIGAPPBOM.Servicio.Administracion.RolUsuarios;
using System.Web.Mvc;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Servicio.Administracion.RolUsuarios;
using System.Collections;

namespace SIGAPPBOM.Web.Unit.Test.Administracion
{



    [TestFixture]
    class RolUsuarioControllerTest
    {
        private Mock<IRolUsuarioService> rolusuarioServiceFalso;
        private Mock<IAuthenticationService> authenticationServiceFalso;
        private RolUsuarioController rolusuarioController;
        [SetUp]
        public void SetUp()
        {
            rolusuarioServiceFalso = new Mock<IRolUsuarioService>();
            authenticationServiceFalso = new Mock<IAuthenticationService>();

            var user = new UserPrincipal(new UserIdentity(1, "jperez"), new ArrayList());
            authenticationServiceFalso.Setup(x => x.ObtienerInformacionUsuario()).Returns(user);

            rolusuarioController = new RolUsuarioController(rolusuarioServiceFalso.Object, authenticationServiceFalso.Object);
        }
        [Test]
        [Category("Ver Pantalla Lista de RolUsuarios")]
        public void MostrarRolUsuarios_CUANDO_NavegoAAdministracionDeRolUsuarios_ENTONCES_MiPantallaSeDebeLLamarListaDeRolUsuarios()
        {
            rolusuarioServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<RolUsuarioViewModel>());

            var viewResult = (ViewResult)rolusuarioController.MostrarRolUsuario();
            var model = (List<RolUsuarioViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de RolUsuarios", viewBag.Titulo);
        }
    }

    

}
