using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using NHibernate;
using SIGAPPBOM.Logistica.Dominio.Comun;
using SIGAPPBOM.Logistica.NHibernate.Mapeo;

namespace SIGAPPBOM.Logistica.NHibernate
{
    public class NHibernateConfigurator
    {
        public static ISessionFactory SessionFactory { get; set; }

        public static void Configure()
        {            

            var model = AutoMap.AssemblyOf<Entidad>(new AutomappingConfiguration())                
                .IgnoreBase<Entidad>()
                .Conventions.AddFromAssemblyOf<IdConvention>();

            SessionFactory=  Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                .ConnectionString(x => x.FromConnectionStringWithKey("SigAppbomDB"))
                    ).Mappings(x => x.AutoMappings.Add(model))                    
                    .BuildSessionFactory();

        }

        public static ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
