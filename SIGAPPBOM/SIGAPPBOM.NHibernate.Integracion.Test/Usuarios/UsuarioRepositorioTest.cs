using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.Infraestructura.UnitOfWork;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio;
using SIGAPPBOM.Dominio.Administracion.Usuarios;
using SIGAPPBOM.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.NHibernate.Integracion.Test.Usuarios
{
    [TestFixture]
    class UsuarioRepositorioTest
    {
        private UnitOfWork UnitOfWork;
        private IRepositorio<Usuario> usuarioRepositorio;

        [SetUp]
        public void Setup()
        {
            ServicioNHibernate.Start();
            AutoMapperConfiguration.Start();
            DependencyConfigurator.Start();
            UnitOfWork = (UnitOfWork)ObjectFactory.GetInstance<IUnitOfWork>();
            UnitOfWork.Begin();
            usuarioRepositorio = new Repositorio<Usuario>(UnitOfWork);
        }

        [Test]
        public void Grabar_Cuando_GraboUnUsuario_Entonces_SeGrabaraEnLaBaseDeDatos()
        {
            var usuario = new Usuario
            {
                Id = 0,
                Nombre = "admin",
				Password="admin"
            };

            usuarioRepositorio.Guardar(usuario);

            Usuario usuarioGuardado = usuarioRepositorio.BuscarPor(usuario.Id);

            Assert.AreEqual("admin", usuarioGuardado.Nombre);
            Assert.AreEqual("admin", usuarioGuardado.Password);

            usuarioRepositorio.Eliminar(usuarioGuardado);
        }


        [TearDown]
        public void TearDown()
        {
            UnitOfWork.End();
        }


    }
}
