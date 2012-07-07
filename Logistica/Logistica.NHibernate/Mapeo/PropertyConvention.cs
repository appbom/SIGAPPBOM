using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace SIGAPPBOM.Logistica.NHibernate.Mapeo
{
    public class PropertyConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            // instance.Not.Nullable();
        }

    }
}
