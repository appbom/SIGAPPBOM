using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace SIGAPPBOM.NHibernate.Mapeo
{
    public class PropertyConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            // instance.Not.Nullable();
        }

    }
}
