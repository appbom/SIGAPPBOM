using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Web.Areas.Administracion.Controllers;
using SIGAPPBOM.Infraestructura.Authentication;
using Moq;
using SIGAPPBOM.Servicio.Administracion.Menues;
using System.Web.Mvc;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Servicio.Administracion.Menues;
using System.Collections;

namespace SIGAPPBOM.Web.Unit.Test.Administracion
{



    [TestFixture]
    class MenuControllerTest
    {
        private Mock<IMenuService> menuServiceFalso;
        private Mock<IAuthenticationService> authenticationServiceFalso;
        private MenuController menuController;
        [SetUp]
        public void SetUp()
        {
            menuServiceFalso = new Mock<IMenuService>();
            authenticationServiceFalso = new Mock<IAuthenticationService>();

            var user = new UserPrincipal(new UserIdentity(1, "jperez"), new ArrayList());
            authenticationServiceFalso.Setup(x => x.ObtienerInformacionUsuario()).Returns(user);

            menuController = new MenuController(menuServiceFalso.Object, authenticationServiceFalso.Object);
        }
        [Test]
        [Category("Ver Pantalla Lista de Menues")]
        public void MostrarMenues_CUANDO_NavegoAAdministracionDeMenues_ENTONCES_MiPantallaSeDebeLLamarListaDeMenues()
        {
            menuServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<MenuViewModel>());

            var viewResult = (ViewResult)menuController.MostrarMenu();
            var model = (List<MenuViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Menues", viewBag.Titulo);
        }
    }

    

}
