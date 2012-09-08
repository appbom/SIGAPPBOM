using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio;
using SIGAPPBOM.Dominio.Administracion.Parcelas;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.NHibernate.Integracion.Test.Parcelas
{
    [TestFixture]
    class ParcelaRepositorioTest
    {
        private UnitOfWork UnitOfWork;
        private IRepositorio<Parcela> parcelaRepositorio;

        [SetUp]
        public void Setup()
        {
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            UnitOfWork.Begin();
            parcelaRepositorio = new Repositorio<Parcela>(UnitOfWork);
        }

        [Test]
        public void Grabar_Cuando_GraboUnaParcela_Entonces_SeGrabaraEnLaBaseDeDatos()
        {
            var parcela = new Parcela
            {
                Id = 0,
                Sector = "Lado Derecho",
				Tamaño = 2,
                IdProductor = 0001
            };

            parcelaRepositorio.Guardar(parcela);

            Parcela parcelaGuardado = parcelaRepositorio.BuscarPor(parcela.Id);

            Assert.AreEqual("Lado Derecho", parcelaGuardado.Sector);
            Assert.AreEqual(2, parcelaGuardado.Tamaño);
			Assert.AreEqual(0001, parcelaGuardado.IdProductor);

            parcelaRepositorio.Eliminar(parcelaGuardado);
        }


        [TearDown]
        public void TearDown()
        {
            UnitOfWork.End();
        }


    }
}
