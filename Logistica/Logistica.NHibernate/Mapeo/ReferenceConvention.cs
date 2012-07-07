using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace SIGAPPBOM.Logistica.NHibernate.Mapeo
{
    public class ReferenceConvention: IReferenceConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Column(instance.Name + "Id");
            instance.Cascade.SaveUpdate();
        }
    }
}
