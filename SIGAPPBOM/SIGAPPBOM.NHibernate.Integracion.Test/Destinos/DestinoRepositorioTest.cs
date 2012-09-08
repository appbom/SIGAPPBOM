using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio;
using SIGAPPBOM.Dominio.Administracion.Destinos;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.NHibernate.Integracion.Test.Destinos
{
    [TestFixture]
    class DestinoRepositorioTest
    {
        private UnitOfWork UnitOfWork;
        private IRepositorio<Destino> destinoRepositorio;

        [SetUp]
        public void Setup()
        {
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            UnitOfWork.Begin();
            destinoRepositorio = new Repositorio<Destino>(UnitOfWork);
        }

        [Test]
        public void Grabar_Cuando_GraboUnDestino_Entonces_SeGrabaraEnLaBaseDeDatos()
        {
            var destino = new Destino
            {
                Id = 0,
                Nombre = "Japon"
            };

            destinoRepositorio.Guardar(destino);

            Destino destinoGuardado = destinoRepositorio.BuscarPor(destino.Id);

            Assert.AreEqual("Japon", destinoGuardado.Nombre);

            destinoRepositorio.Eliminar(destinoGuardado);
        }


        [TearDown]
        public void TearDown()
        {
            UnitOfWork.End();
        }


    }
}
