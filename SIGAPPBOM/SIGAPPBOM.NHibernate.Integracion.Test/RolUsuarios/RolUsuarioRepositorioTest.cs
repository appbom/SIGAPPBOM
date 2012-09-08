using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio;
using SIGAPPBOM.Dominio.Administracion.RolUsuarios;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.NHibernate.Integracion.Test.RolUsuarios
{
    [TestFixture]
    class RolUsuarioRepositorioTest
    {
        private UnitOfWork UnitOfWork;
        private IRepositorio<RolUsuario> rolusuarioRepositorio;

        [SetUp]
        public void Setup()
        {
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            UnitOfWork.Begin();
            rolusuarioRepositorio = new Repositorio<RolUsuario>(UnitOfWork);
        }

        [Test]
        public void Grabar_Cuando_GraboUnRolUsuario_Entonces_SeGrabaraEnLaBaseDeDatos()
        {
            var rolusuario = new RolUsuario
            {
                Id = 0,
                IdUsuario= 1,
				IdRol=1
            };

            rolusuarioRepositorio.Guardar(rolusuario);

            RolUsuario rolusuarioGuardado = rolusuarioRepositorio.BuscarPor(rolusuario.Id);

            Assert.AreEqual(1, rolusuarioGuardado.IdUsuario);
			Assert.AreEqual(1, rolusuarioGuardado.IdRol);

            rolusuarioRepositorio.Eliminar(rolusuarioGuardado);
        }


        [TearDown]
        public void TearDown()
        {
            UnitOfWork.End();
        }


    }
}
