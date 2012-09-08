using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Web.Areas.Administracion.Controllers;
using SIGAPPBOM.Infraestructura.Authentication;
using Moq;
using SIGAPPBOM.Servicio.Administracion.Parcelas;
using System.Web.Mvc;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Servicio.Administracion.Parcelas;
using System.Collections;

namespace SIGAPPBOM.Web.Unit.Test.Administracion
{



    [TestFixture]
    class ParcelaControllerTest
    {
        private Mock<IParcelaService> parcelaServiceFalso;
        private Mock<IAuthenticationService> authenticationServiceFalso;
        private ParcelaController parcelaController;
        [SetUp]
        public void SetUp()
        {
            parcelaServiceFalso = new Mock<IParcelaService>();
            authenticationServiceFalso = new Mock<IAuthenticationService>();

            var user = new UserPrincipal(new UserIdentity(1, "jperez"), new ArrayList());
            authenticationServiceFalso.Setup(x => x.ObtienerInformacionUsuario()).Returns(user);

            parcelaController = new ParcelaController(parcelaServiceFalso.Object, authenticationServiceFalso.Object);
        }
        [Test]
        [Category("Ver Pantalla Lista de Parcelas")]
        public void MostrarParcelas_CUANDO_NavegoAAdministracionDeParcelas_ENTONCES_MiPantallaSeDebeLLamarListaDeParcelas()
        {
            parcelaServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<ParcelaViewModel>());

            var viewResult = (ViewResult)parcelaController.MostrarParcela();
            var model = (List<ParcelaViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Parcelas", viewBag.Titulo);
        }
    }

    

}
