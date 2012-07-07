using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using SIGAPPBOM.Logistica.Dominio.Comun;
using SIGAPPBOM.Logistica.Infraestructura.UnitOfWork;

namespace SIGAPPBOM.Logistica.NHibernate.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : Entidad
    {
        #region UnitOfWork

        private readonly IUnitOfWork unitOfWork;
        private ISession Session
        {
            get
            {
                return this.unitOfWork.CurrentSession;
            }
        }
        public Repositorio(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Metodos

        public void Guardar(T T)
        {
            this.Session.SaveOrUpdate(T);
        }

        public T BuscarPor(Int32 id)
        {
            return this.Session.Get<T>(id);
        }

        public IQueryable<T> TraerTodo()
        {
            return this.Session.Query<T>();
        }

        public void Eliminar(T obj)
        {
            this.Session.Delete(obj);
        }

        #endregion


        public T Load(int id)
        {
            return this.Session.Load<T>(id);
        }
    }
}
