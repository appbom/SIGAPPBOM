using NHibernate;
using SIGAPPBOM.NHibernate;

namespace SIGAPPBOM.Servicio
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
