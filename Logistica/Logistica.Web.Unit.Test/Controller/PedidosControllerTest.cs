using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SIGAPPBOM.Logistica.Web.Controllers;
using SIGAPPBOM.Logistica.Servicio.Pedidos;
using SIGAPPBOM.Logistica.Servicio.ViewModels;

namespace SIGAPPBOM.Logistica.Web.Unit.Test.Controller
{
    [TestFixture]
    public class PedidosControllerTest
    {
        private PedidosController pedidosController;
        private Mock<IPedidoBienesService> pedidoBienesServiceFalso;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            pedidoBienesServiceFalso = new Mock<IPedidoBienesService>();
            pedidosController = new PedidosController(pedidoBienesServiceFalso.Object);
        }

        [TearDown]
        public void Dispose()
        {
            pedidoBienesServiceFalso = null;
            pedidosController = null;
        }

        #endregion

        #region Tests

        [Test]
        [Category("Pedidos Logistica")]
        public void PedidosLogistica_CUANDO_ElUsuarioPedroDiazIngresaAPedidosLogistica_ENTONCES_SeDebeMostrarLaPantallaPedidosLogistica()
        {
            var pedidos = new List<PedidoViewModel>();
            pedidoBienesServiceFalso.Setup(x => x.TraerLista()).Returns(pedidos);
            var viewResult = (ViewResult)pedidosController.PedidosLogistica();

            Assert.AreEqual("Pedidos Logística", viewResult.ViewBag.Titulo);
        }

        [Test]
        [Category("Ver Lista de Pedidos Vacia")]
        public void PedidosLogistica_CUANDO_ElUsuarioPedroDiazIngresaAPedidosLogisticaYNoHayPedidosRegistrados_ENTONCES_SeDebeMostrarLaListaDePedidosVacía()
        {
            var pedidos = new List<PedidoViewModel>();
            pedidoBienesServiceFalso.Setup(x => x.TraerLista()).Returns(pedidos);

            var viewResult = (ViewResult)pedidosController.PedidosLogistica();
            var model = (List<PedidoViewModel>)viewResult.Model;

            Assert.AreEqual("Pedidos Logística", viewResult.ViewBag.Titulo);
            Assert.AreEqual(0, model.Count);
        }

        [Test]
        [Category("Ver Lista de Pedidos Con Elementos")]
        public void PedidosLogistica_CUANDO_ElUsuarioPedroDiazIngresaAPedidosLogisticaYHayDosPedidosRegistrados_ENTONCES_SeDebeMostrarLaListaDePedidosConDosElementos()
        {
            var pedidos = new List<PedidoViewModel>
                              {
                                  new PedidoViewModel{Id = 103, Descripcion = "Pedido 03", Solicitante = "José Díaz",FechaCreacion = DateTime.Parse("10/05/2012")},
                                  new PedidoViewModel{Id = 102, Descripcion = "Pedido 02", Solicitante = "Pedro Flores",FechaCreacion = DateTime.Parse("09/05/2012")},
                                  new PedidoViewModel{Id = 101, Descripcion = "Pedido 01", Solicitante = "Manuel Farfán",FechaCreacion = DateTime.Parse("08/05/2012")}
                              };
            pedidoBienesServiceFalso.Setup(x => x.TraerLista()).Returns(pedidos);

            var viewResult = (ViewResult)pedidosController.PedidosLogistica();
            var model = (List<PedidoViewModel>)viewResult.Model;

            Assert.AreEqual("Pedidos Logística", viewResult.ViewBag.Titulo);
            Assert.AreEqual(3, model.Count);
        }

        #endregion
    }
}
