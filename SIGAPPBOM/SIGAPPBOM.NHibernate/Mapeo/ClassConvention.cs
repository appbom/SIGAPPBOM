using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace SIGAPPBOM.NHibernate.Mapeo
{
    public class ClassConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {         
            instance.Table(instance.EntityType.Name);
        }

    }
}
