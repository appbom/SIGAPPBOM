using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Web.Areas.Administracion.Controllers;
using SIGAPPBOM.Infraestructura.Authentication;
using Moq;
using SIGAPPBOM.Servicio.Administracion.Productores;
using System.Web.Mvc;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Servicio.Administracion.Productores;
using System.Collections;

namespace SIGAPPBOM.Web.Unit.Test.Administracion
{



    [TestFixture]
    class ProductorControllerTest
    {
        private Mock<IProductorService> productorServiceFalso;
        private Mock<IAuthenticationService> authenticationServiceFalso;
        private ProductorController productorController;
        [SetUp]
        public void SetUp()
        {
            productorServiceFalso = new Mock<IProductorService>();
            authenticationServiceFalso = new Mock<IAuthenticationService>();

            var user = new UserPrincipal(new UserIdentity(1, "jperez"), new ArrayList());
            authenticationServiceFalso.Setup(x => x.ObtienerInformacionUsuario()).Returns(user);

            productorController = new ProductorController(productorServiceFalso.Object, authenticationServiceFalso.Object);
        }
        [Test]
        [Category("Ver Pantalla Lista de Productores")]
        public void MostrarProductores_CUANDO_NavegoAAdministracionDeProductores_ENTONCES_MiPantallaSeDebeLLamarListaDeProductores()
        {
            productorServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<ProductorViewModel>());

            var viewResult = (ViewResult)productorController.MostrarProductor();
            var model = (List<ProductorViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Productores", viewBag.Titulo);
        }
    }

    

}
