using System;
using System.Linq;
using SIGAPPBOM.Logistica.Dominio.Comun;

namespace SIGAPPBOM.Logistica.NHibernate.Repositorios
{
    public interface IRepositorio<T> where T : Entidad
    {
        void Guardar(T T);
        T BuscarPor(Int32 id);
        IQueryable<T> TraerTodo();
        void Eliminar(T obj);
        T Load(Int32 id);
    }
}