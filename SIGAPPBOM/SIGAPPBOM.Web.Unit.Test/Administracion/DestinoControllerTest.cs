using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Web.Areas.Administracion.Controllers;
using SIGAPPBOM.Infraestructura.Authentication;
using Moq;
using SIGAPPBOM.Servicio.Administracion.Destinos;
using System.Web.Mvc;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Servicio.Administracion.Destinos;
using System.Collections;

namespace SIGAPPBOM.Web.Unit.Test.Administracion
{



    [TestFixture]
    class DestinoControllerTest
    {
        private Mock<IDestinoService> destinoServiceFalso;
        private Mock<IAuthenticationService> authenticationServiceFalso;
        private DestinoController destinoController;
        [SetUp]
        public void SetUp()
        {
            destinoServiceFalso = new Mock<IDestinoService>();
            authenticationServiceFalso = new Mock<IAuthenticationService>();

            var user = new UserPrincipal(new UserIdentity(1, "jperez"), new ArrayList());
            authenticationServiceFalso.Setup(x => x.ObtienerInformacionUsuario()).Returns(user);

            destinoController = new DestinoController(destinoServiceFalso.Object, authenticationServiceFalso.Object);
        }
        [Test]
        [Category("Ver Pantalla Lista de Destinos")]
        public void MostrarDestinos_CUANDO_NavegoAAdministracionDeDestinos_ENTONCES_MiPantallaSeDebeLLamarListaDeDestinos()
        {
            destinoServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<DestinoViewModel>());

            var viewResult = (ViewResult)destinoController.MostrarDestino();
            var model = (List<DestinoViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Destinos", viewBag.Titulo);
        }
    }

    

}
