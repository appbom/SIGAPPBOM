using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using uNhAddIns.Inflector;

namespace SIGAPPBOM.Logistica.NHibernate.Mapeo
{
    public class ClassConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {         
            instance.Table(instance.EntityType.Name);
        }

    }
}
