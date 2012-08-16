using NUnit.Framework;
using AutoMapper;
using SIGAPPBOM.Web.Bootstraper;

namespace SIGAPPBOM.Servicio.Unit.Test
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
