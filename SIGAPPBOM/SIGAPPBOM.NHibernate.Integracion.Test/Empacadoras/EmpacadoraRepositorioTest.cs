using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio;
using SIGAPPBOM.Dominio.Administracion.Empacadoras;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.NHibernate.Integracion.Test.Empacadoras
{
    [TestFixture]
    class EmpacadoraRepositorioTest
    {
        private UnitOfWork UnitOfWork;
        private IRepositorio<Empacadora> empacadoraRepositorio;

        [SetUp]
        public void Setup()
        {
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            UnitOfWork.Begin();
            empacadoraRepositorio = new Repositorio<Empacadora>(UnitOfWork);
        }

        [Test]
        public void Grabar_Cuando_GraboUnaEmpacadora_Entonces_SeGrabaraEnLaBaseDeDatos()
        {
            var empacadora = new Empacadora
            {
                Id = 0,
                Nombre = "MN1"
            };

            empacadoraRepositorio.Guardar(empacadora);

            Empacadora empacadoraGuardado = empacadoraRepositorio.BuscarPor(empacadora.Id);

            Assert.AreEqual("MN1", empacadoraGuardado.Nombre);

            empacadoraRepositorio.Eliminar(empacadoraGuardado);
        }


        [TearDown]
        public void TearDown()
        {
            UnitOfWork.End();
        }


    }
}
