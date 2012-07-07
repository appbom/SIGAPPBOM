using System;
using NHibernate;

namespace SIGAPPBOM.Logistica.Infraestructura.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory sessionFactory;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public ISession CurrentSession { get; private set; }

        public void Begin()
        {
            CurrentSession = sessionFactory.OpenSession();
            CurrentSession.BeginTransaction();
        }

        public void End()
        {
            try
            {
                if (CurrentSession.Transaction.IsActive)
                    CurrentSession.Transaction.Commit();
            }
            catch (Exception)
            {
                CurrentSession.Transaction.Rollback();
                throw;
            }
            finally
            {
                this.Dispose();
            }
        }

        public void Dispose()
        {
            if (CurrentSession != null)
            {
                if (CurrentSession.Transaction!=null)
                {
                    CurrentSession.Transaction.Dispose();
                }


                CurrentSession.Dispose();
                CurrentSession = null;
            }
        }
    }
}