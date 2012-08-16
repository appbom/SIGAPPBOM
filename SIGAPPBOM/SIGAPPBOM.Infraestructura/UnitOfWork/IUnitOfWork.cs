using System;
using NHibernate;

namespace SIGAPPBOM.Infraestructura.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
         ISession CurrentSession { get; }

        void Begin();
        void End();
    }
}
