using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;
using SIGAPPBOM.Dominio.Administracion;
using SIGAPPBOM.Web.Areas.Administracion.Controllers;
using SIGAPPBOM.Infraestructura.Authentication;
using Moq;
using SIGAPPBOM.Servicio.Administracion.Personas;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Web.Unit.Test.Administracion
{
    [TestFixture]
    public class PersonaControllerTest
    {
        Mock<IAuthenticationService> authenticationserviceFalse;
        Mock<IPersonaService> personaServiceFalse;
        private PersonaController personaController;
        [SetUp]
        public void Setup()
        {
            authenticationserviceFalse = new Mock<IAuthenticationService>();
            personaServiceFalse=new Mock<IPersonaService>();
            personaController = new PersonaController(personaServiceFalse.Object,authenticationserviceFalse.Object);
        }
        [Test]
        [Category("Ver pantalla de Lista de Personas")]
        public void MostrarPersonas_CUANDO_NavegoAPersona_ENTONCES_MiPantallaSeDebeLlamarListadePersonas()
        {
            personaServiceFalse.Setup(x => x.TraerTodo()).Returns(new List<PersonaViewModel>());
            var viewResult=(ViewResult)personaController.MostrarPersona();
            var viewBag=viewResult.ViewBag;
            Assert.AreEqual("Lista de Personas",viewBag.Titulo);

        }

        [Test]
        [Category("Lista de Personas Vacia")]
        public void MostrarPersonas_CUANDO_NavegoAPersonaYNoExistePersonasRegistradas_ENTONCES_SeDebeMostrarUnMensajeNoHayPersonasRegistradas()
        {
            personaServiceFalse.Setup(x => x.TraerTodo()).Returns(new List<PersonaViewModel>());

            var viewResult = (ViewResult)personaController.MostrarPersona();
            var listaModel = (List<PersonaViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Personas", viewBag.Titulo);
            Assert.AreEqual("No hay Personas registradas", viewBag.Mensaje);
            Assert.AreEqual(0, listaModel.Count);
        }

        [Test]
        [Category("Lista de Personas con elementos")]
        public void MostrarPersonas_CUANDO_NavegoAPersonaYExistenTresPersonasRegistradas_ENTONCES_SeDebeMostrarUnaListaConTresElementos()
        {
            personaServiceFalse.Setup(x => x.TraerTodo()).Returns(new List<PersonaViewModel> {
                                                                                                new PersonaViewModel(),
                                                                                                new PersonaViewModel(),
                                                                                                new PersonaViewModel()
                                                                                               });

            var viewResult = (ViewResult)personaController.MostrarPersona();
            var listaModel = (List<PersonaViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Personas", viewBag.Titulo);
            Assert.IsNull(viewBag.Mensaje);
            Assert.AreEqual(3, listaModel.Count);
        }

    }
}
