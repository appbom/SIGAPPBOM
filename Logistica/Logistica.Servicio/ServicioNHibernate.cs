using NHibernate;
using SIGAPPBOM.Logistica.NHibernate;

namespace SIGAPPBOM.Logistica.Servicio
{
    public class ServicioNHibernate
    {
        public static ISessionFactory SessionFactory()
        {
            return NHibernateConfigurator.SessionFactory;
        }

        public static void Start()
        {
            NHibernateConfigurator.Configure();
        }
    }
}
