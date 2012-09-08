using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio;
using SIGAPPBOM.Dominio.Administracion.Roles;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.NHibernate.Integracion.Test.Roles
{
    [TestFixture]
    class RolRepositorioTest
    {
        private UnitOfWork UnitOfWork;
        private IRepositorio<Rol> rolRepositorio;

        [SetUp]
        public void Setup()
        {
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            UnitOfWork.Begin();
            rolRepositorio = new Repositorio<Rol>(UnitOfWork);
        }

        [Test]
        public void Grabar_Cuando_GraboUnRol_Entonces_SeGrabaraEnLaBaseDeDatos()
        {
            var rol = new Rol
            {
                Id = 0,
                Nombre = "Administrador"
            };

            rolRepositorio.Guardar(rol);

            Rol rolGuardado = rolRepositorio.BuscarPor(rol.Id);

            Assert.AreEqual("Administrador", rolGuardado.Nombre);

            rolRepositorio.Eliminar(rolGuardado);
        }


        [TearDown]
        public void TearDown()
        {
            UnitOfWork.End();
        }


    }
}
