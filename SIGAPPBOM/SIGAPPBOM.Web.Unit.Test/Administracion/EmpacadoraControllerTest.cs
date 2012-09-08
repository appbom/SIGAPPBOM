using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Web.Areas.Administracion.Controllers;
using SIGAPPBOM.Infraestructura.Authentication;
using Moq;
using SIGAPPBOM.Servicio.Administracion.Empacadoras;
using System.Web.Mvc;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Servicio.Administracion.Empacadoras;
using System.Collections;

namespace SIGAPPBOM.Web.Unit.Test.Administracion
{



    [TestFixture]
    class EmpacadoraControllerTest
    {
        private Mock<IEmpacadoraService> empacadoraServiceFalso;
        private Mock<IAuthenticationService> authenticationServiceFalso;
        private EmpacadoraController empacadoraController;
        [SetUp]
        public void SetUp()
        {
            empacadoraServiceFalso = new Mock<IEmpacadoraService>();
            authenticationServiceFalso = new Mock<IAuthenticationService>();

            var user = new UserPrincipal(new UserIdentity(1, "jperez"), new ArrayList());
            authenticationServiceFalso.Setup(x => x.ObtienerInformacionUsuario()).Returns(user);

            empacadoraController = new EmpacadoraController(empacadoraServiceFalso.Object, authenticationServiceFalso.Object);
        }
        [Test]
        [Category("Ver Pantalla Lista de Empacadoras")]
        public void MostrarEmpacadoras_CUANDO_NavegoAAdministracionDeEmpacadoras_ENTONCES_MiPantallaSeDebeLLamarListaDeEmpacadoras()
        {
            empacadoraServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<EmpacadoraViewModel>());

            var viewResult = (ViewResult)empacadoraController.MostrarEmpacadora();
            var model = (List<EmpacadoraViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Empacadoras", viewBag.Titulo);
        }
    }

    

}
