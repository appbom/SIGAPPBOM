using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Web.Areas.Administracion.Controllers;
using SIGAPPBOM.Infraestructura.Authentication;
using Moq;
using SIGAPPBOM.Servicio.Administracion.Roles;
using System.Web.Mvc;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Servicio.Administracion.Roles;
using System.Collections;

namespace SIGAPPBOM.Web.Unit.Test.Administracion
{



    [TestFixture]
    class RolControllerTest
    {
        private Mock<IRolService> rolServiceFalso;
        private Mock<IAuthenticationService> authenticationServiceFalso;
        private RolController rolController;
        [SetUp]
        public void SetUp()
        {
            rolServiceFalso = new Mock<IRolService>();
            authenticationServiceFalso = new Mock<IAuthenticationService>();

            var user = new UserPrincipal(new UserIdentity(1, "jperez"), new ArrayList());
            authenticationServiceFalso.Setup(x => x.ObtienerInformacionUsuario()).Returns(user);

            rolController = new RolController(rolServiceFalso.Object, authenticationServiceFalso.Object);
        }
        [Test]
        [Category("Ver Pantalla Lista de Roles")]
        public void MostrarRoles_CUANDO_NavegoAAdministracionDeRoles_ENTONCES_MiPantallaSeDebeLLamarListaDeRoles()
        {
            rolServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<RolViewModel>());

            var viewResult = (ViewResult)rolController.MostrarRol();
            var model = (List<RolViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Roles", viewBag.Titulo);
        }
    }

    

}
