using System;
using NHibernate;

namespace SIGAPPBOM.Logistica.Infraestructura.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
         ISession CurrentSession { get; }

        void Begin();
        void End();
    }
}
