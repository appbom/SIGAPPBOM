using AutoMapper;
using NUnit.Framework;
using SIGAPPBOM.Logistica.Web.Bootstraper;
using StructureMap;

namespace SIGAPPBOM.Logistica.Web.Unit.Test.Mapeo
{
    [TestFixture]
    public class ConfigurationTest
    {

        [Test]
        public void AutomapperConfigurationWithIoC()
        {
            Mapper.CreateMap<Origen, Destino>();
            Mapper.AssertConfigurationIsValid();

            ObjectFactory.Initialize(x => x.For<IMappingEngine>().Use(Mapper.Engine));



            var engine = ObjectFactory.GetInstance<IMappingEngine>();


            var destination = engine.Map<Origen, Destino>(new Origen { valor = 12 });
            //var destination=Mapper.Map<Origen, Destino>(new Origen { valor = 12 });
            Assert.AreEqual(12, destination.valor);
        }



        [Test]
        public void AutomapperConfiguration_TestDomainViewModelMapping()
        {
            AutoMapperConfiguration.Start();

            Mapper.AssertConfigurationIsValid();


        }

    }



}
