using SIGAPPBOM.Dominio.Comun;

namespace SIGAPPBOM.Dominio.Administracion.Usuarios
{
    public class Usuario : Entidad
    {
        public virtual string Nombre { get; set; }
		public virtual string Password { get; set; }
    }
}
