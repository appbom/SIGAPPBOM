using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using SIGAPPBOM.NHibernate.Repositorios;
using NUnit.Framework;
using SIGAPPBOM.Dominio.Administracion.RolUsuarios;
using SIGAPPBOM.Servicio.Administracion.RolUsuarios;

namespace SIGAPPBOM.Servicio.Unit.Test.RolUsuarios
{
    [TestFixture]
    class RolUsuarioServiceTest : ServiciosTest
    {
        private Mock<IRepositorio<RolUsuario>> rolesusuariosRepositorioFalso;
        private RolUsuarioService rolusuarioService;

        #region SetUp / TearDown

        [SetUp]
        public void Init()
        {
            rolesusuariosRepositorioFalso = new Mock<IRepositorio<RolUsuario>>();
            rolusuarioService = new RolUsuarioService(rolesusuariosRepositorioFalso.Object, mappingEngine);
        }

        [TearDown]
        public void Dispose()
        {
            rolesusuariosRepositorioFalso = null;
            rolusuarioService = null;
        }

        #endregion

        
    }
}
