using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio;
using SIGAPPBOM.Dominio.Administracion.Menues;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.NHibernate.Integracion.Test.Menues
{
    [TestFixture]
    class MenuRepositorioTest
    {
        private UnitOfWork UnitOfWork;
        private IRepositorio<Menu> menuRepositorio;

        [SetUp]
        public void Setup()
        {
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            UnitOfWork.Begin();
            menuRepositorio = new Repositorio<Menu>(UnitOfWork);
        }

        [Test]
        public void Grabar_Cuando_GraboUnMenu_Entonces_SeGrabaraEnLaBaseDeDatos()
        {
            var menu = new Menu
            {
                Id = 0,
                Titulo = "RolUsuarios",
				Controlador = "RolUsuario",
                Accion = "MostrarRolUsuario",
				Imagen="icon-home icon-white"
            };

            menuRepositorio.Guardar(menu);

            Menu menuGuardado = menuRepositorio.BuscarPor(menu.Id);

            Assert.AreEqual("RolUsuarios", menuGuardado.Titulo);
            Assert.AreEqual("RolUsuario", menuGuardado.Controlador);
			Assert.AreEqual("MostrarRolUsuario", menuGuardado.Accion);
			Assert.AreEqual("icon-home icon-white", menuGuardado.Imagen);

            menuRepositorio.Eliminar(menuGuardado);
        }


        [TearDown]
        public void TearDown()
        {
            UnitOfWork.End();
        }


    }
}
