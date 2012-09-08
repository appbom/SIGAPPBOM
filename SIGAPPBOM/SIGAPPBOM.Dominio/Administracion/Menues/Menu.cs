using SIGAPPBOM.Dominio.Comun;

namespace SIGAPPBOM.Dominio.Administracion.Menues
{
    public class Menu : Entidad
    {
        public virtual string Titulo { get; set; }
		public virtual string Controlador { get; set; }
		public virtual string Accion { get; set; }
		public virtual string Imagen { get; set; }
    }
}
