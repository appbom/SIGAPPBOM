using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SIGAPPBOM.Infraestructura.Authentication;
using SIGAPPBOM.Servicio.Logistica.Articulos;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Web.Areas.Administracion.Controllers;

namespace SIGAPPBOM.Web.Unit.Test.Administracion
{
    [TestFixture]
    public class ArticulosControllerTest
    {
        private Mock<IArticuloService> articuloServiceFalso;
        private Mock<IAuthenticationService> authenticationServiceFalso;
        private ArticulosController articulosController;
        [SetUp]
        public void SetUp()
        {
            articuloServiceFalso = new Mock<IArticuloService>();
            authenticationServiceFalso = new Mock<IAuthenticationService>();

            var user = new UserPrincipal(new UserIdentity(1, "jperez"), new ArrayList());
            authenticationServiceFalso.Setup(x => x.ObtienerInformacionUsuario()).Returns(user);

            articulosController = new ArticulosController(articuloServiceFalso.Object, authenticationServiceFalso.Object);
        }

        [Test]
        [Category("Ver Pantalla Lista de Articulos")]
        public void MostrarArticulos_CUANDO_NavegoAAdministracionDeArticulos_ENTONCES_MiPantallaSeDebeLLamarListaDeArticulos()
        {
            articuloServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<ArticuloViewModel>());

            var viewResult = (ViewResult)articulosController.MostrarArticulos();
            var model = (List<ArticuloViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Artículos", viewBag.Titulo);
        }

        [Test]
        [Category("Lista de Articulos Vacía")]
        public void MostrarArticulos_CUANDO_NavegoAAdministracionDeArticulosYNoHayArticulosRegistrados_ENTONCES_SeDebeMostrarMensajeNoHayArticulosRegistrados()
        {
            articuloServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<ArticuloViewModel>());

            var viewResult = (ViewResult)articulosController.MostrarArticulos();
            var lista = (List<ArticuloViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Artículos", viewBag.Titulo);
            Assert.AreEqual(0, lista.Count);
            Assert.AreEqual("No hay artículos registrados", viewBag.Mensaje);
        }

        [Test]
        [Category("Lista de Articulos Con Elementos")]
        public void MostrarArticulos_CUANDO_NavegoAAdministracionDeArticulosYHay3ArticulosRegistrados_ENTONCES_SeDebeMostrarUnaListaCon3Articulos()
        {
            articuloServiceFalso.Setup(x => x.TraerTodo()).Returns(new List<ArticuloViewModel>
                                                                       {
                                                                           new ArticuloViewModel{Id = 1},
                                                                           new ArticuloViewModel{Id = 2},
                                                                           new ArticuloViewModel{Id = 3},
                                                                       });

            var viewResult = (ViewResult)articulosController.MostrarArticulos();
            var lista = (List<ArticuloViewModel>)viewResult.Model;
            var viewBag = viewResult.ViewBag;

            Assert.AreEqual("Lista de Artículos", viewBag.Titulo);
            Assert.AreEqual(3, lista.Count);
            Assert.IsNull(viewBag.Mensaje);
        }
    }
}
