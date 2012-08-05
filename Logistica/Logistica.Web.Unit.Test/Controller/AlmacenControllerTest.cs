using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using SIGAPPBOM.Logistica.Infraestructura.Authentication;
using SIGAPPBOM.Logistica.Servicio.Almacen;
using SIGAPPBOM.Logistica.Servicio.Pedidos;
using SIGAPPBOM.Logistica.Servicio.ViewModels;
using Moq;
using SIGAPPBOM.Logistica.Web.Controllers;

namespace SIGAPPBOM.Logistica.Web.Unit.Test.Controller
{
    [TestFixture]
    public class AlmacenControllerTest
    {
        private Mock<IPedidoInsumosService> pedidoInsumosServiceFalso;
        private Mock<IAuthenticationService> autenticacionServiceFalso;
        private Mock<IAlmacenService> almacenServiceFalso;
        private SalidaViewModel salidaViewModel;
        private List<string> errores;
        private AlmacenController almacenController;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            pedidoInsumosServiceFalso = new Mock<IPedidoInsumosService>();
            almacenServiceFalso = new Mock<IAlmacenService>();
            autenticacionServiceFalso = new Mock<IAuthenticationService>();

            salidaViewModel = new SalidaViewModel
                                      {
                                          Id = 1,
                                          Usuario = "LDIAZ",
                                          PedidoId = 1,
                                          PedidoSolicitante = "jperez",
                                          Fecha = DateTime.Parse("01/05/2012")
                                      };
            almacenController = new AlmacenController(almacenServiceFalso.Object, pedidoInsumosServiceFalso.Object, autenticacionServiceFalso.Object);
            errores = new List<string>();
        }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Lista de Salidas

        [Test]
        [Category("Ver Pnatalla Salidas de Almacen")]
        public void Salidas_IngresarASalidasAlmacen_Cuando_ElUsuarioJuanPerezIngresaAAlmacenPaita_Entonces_SeDebeMostrarLaPantallaSalidasDeAlmacenPaita()
        {
            almacenServiceFalso.Setup(x => x.TraerSalidas()).Returns(new List<SalidaViewModel>());

            var viewBag = ((ViewResult)almacenController.MostrarSalidas(salidaViewModel.Id)).ViewBag;

            Assert.AreEqual("Almacen", viewBag.Titulo);
            Assert.AreEqual("Salidas de Almacen", viewBag.Accion);
        }

        [Test]
        [Category("Ver Lista de Salidas de Almacen vacía")]
        public void Salidas_VerSalidasDeAlmacen_Cuando_ElAlmaceneroJuanPerezDelAlmacenPaitaNavegaASalidasDeAAlmacenYNotieneSalidasRegistradas_Entonces_SeDebeMostrarUnaListaDeSalidasVacia()
        {
            almacenServiceFalso.Setup(x => x.TraerSalidas()).Returns(new List<SalidaViewModel>());

            var listaSalidas = (List<SalidaViewModel>)((ViewResult)almacenController.MostrarSalidas(salidaViewModel.Id)).Model;
            var viewBag = ((ViewResult)almacenController.MostrarSalidas(salidaViewModel.Id)).ViewBag;
            Assert.AreEqual("Salidas de Almacen", viewBag.Accion);
            Assert.AreEqual("No tiene salidas registradas en el almacen", viewBag.Mensaje);
            Assert.AreEqual(0, listaSalidas.Count);
        }

        [Test]
        [Category("Ver Lista de Salidas de Almacen con elementos")]
        public void Salidas_VerSalidasDeAlmacen_Cuando_ElAlmaceneroJuanPerezDelAlmacenPaitaNavegaASalidasDeAAlmacenYTieneTresSalidasRegistradas_Entonces_SeDebeMostrarUnaListaDeSalidasConTresElementos()
        {
            almacenServiceFalso.Setup(x => x.TraerSalidas()).Returns(new List<SalidaViewModel>
                {
                    new SalidaViewModel{Id=1, Usuario = "LDIAZ", PedidoId = 1, PedidoSolicitante = "jperez", Fecha = DateTime.Parse("01/05/2012")},
                    new SalidaViewModel{Id=2, Usuario = "LDIAZ", PedidoId = 2, PedidoSolicitante = "jperez", Fecha = DateTime.Parse("02/05/2012")},
                    new SalidaViewModel{Id=3, Usuario = "LDIAZ", PedidoId = 3, PedidoSolicitante = "jperez", Fecha = DateTime.Parse("03/05/2012")}
                });


            var listaSalidas = (List<SalidaViewModel>)((ViewResult)almacenController.MostrarSalidas(salidaViewModel.Id)).Model;
            var viewBag = ((ViewResult)almacenController.MostrarSalidas(salidaViewModel.Id)).ViewBag;
            Assert.AreEqual("Salidas de Almacen", viewBag.Accion);
            Assert.AreEqual(3, listaSalidas.Count);
        }

        #endregion
    }
}
