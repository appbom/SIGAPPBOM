using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace SIGAPPBOM.NHibernate.Mapeo
{
    public class IdConvention: IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            var table = instance.EntityType.Name.ToUpper();
            instance.GeneratedBy.HiLo("Claves","NextHi","100",
                x => x.AddParam("where", string.Format("Tabla='{0}'", table)));

            instance.Column("Id");
        }
    }
}
