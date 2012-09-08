using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SIGAPPBOM.NHibernate.Repositorios;
using Moq;
using SIGAPPBOM.Dominio.Administracion.Personas;
using SIGAPPBOM.Servicio.Administracion.Personas;

namespace SIGAPPBOM.Servicio.Unit.Test.APPBOM.Personas
{
    [TestFixture]
    public class PersonaServiceTest:ServiciosTest
    {
        private PersonaService personaService;
        Mock<IRepositorio<Persona>> personaRepositorioFalso;
        [SetUp]
        public void Setup()
        {
            personaRepositorioFalso = new Mock<IRepositorio<Persona>>();
            personaService = new PersonaService(personaRepositorioFalso.Object,mappingEngine);
        }

        [Test]
        public void TraerLista_CUANDO_NOExistanPersonas_ENTONCES_DebeDevolverListaVacia()
        {
            var personas = new EnumerableQuery<Persona>(new List<Persona>());
            personaRepositorioFalso.Setup(x => x.TraerTodo()).Returns(personas);
            var listaResult=personaService.TraerTodo();

            Assert.AreEqual(0,listaResult.Count);
        }
    }
}
