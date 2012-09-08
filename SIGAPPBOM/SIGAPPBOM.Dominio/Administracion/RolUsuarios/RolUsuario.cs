using SIGAPPBOM.Dominio.Comun;

namespace SIGAPPBOM.Dominio.Administracion.RolUsuarios
{
    public class RolUsuario : Entidad
    {
        public virtual int IdUsuario { get; set; }
		public virtual int IdRol { get; set; }
    }
}
