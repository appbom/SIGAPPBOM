using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio;
using SIGAPPBOM.Dominio.Administracion.Productores;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.NHibernate.Integracion.Test.Productores
{
    [TestFixture]
    class ProductorRepositorioTest
    {
        private UnitOfWork UnitOfWork;
        private IRepositorio<Productor> productorRepositorio;

        [SetUp]
        public void Setup()
        {
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            UnitOfWork.Begin();
            productorRepositorio = new Repositorio<Productor>(UnitOfWork);
        }

        [Test]
        public void Grabar_Cuando_GraboUnProductor_Entonces_SeGrabaraEnLaBaseDeDatos()
        {
            var productor = new Productor
            {
                Id = 0,
                Nombre = "Armando Gonzales",
                DNI = 45815611
            };

            productorRepositorio.Guardar(productor);

            Productor productorGuardado = productorRepositorio.BuscarPor(productor.Id);

            Assert.AreEqual("Armando Gonzales", productorGuardado.Nombre);
            Assert.AreEqual(45815611, productorGuardado.DNI);

            productorRepositorio.Eliminar(productorGuardado);
        }


        [TearDown]
        public void TearDown()
        {
            UnitOfWork.End();
        }


    }
}
