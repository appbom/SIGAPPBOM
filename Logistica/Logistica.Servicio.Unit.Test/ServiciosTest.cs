using NUnit.Framework;
using AutoMapper;
using SIGAPPBOM.Logistica.Web.Bootstraper;

namespace SIGAPPBOM.Logistica.Servicio.Unit.Test
{
    [TestFixture]
    public abstract class ServiciosTest
    {

        protected IMappingEngine mappingEngine;

        [TestFixtureSetUp]
        protected void SetupFixture()
        {
            AutoMapperConfiguration.Start();
            mappingEngine = Mapper.Engine;
        }

    }
}
