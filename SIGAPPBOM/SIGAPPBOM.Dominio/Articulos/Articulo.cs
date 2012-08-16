using SIGAPPBOM.Dominio.Comun;

namespace SIGAPPBOM.Dominio.Articulos
{
    public class Articulo : Entidad
    {
        public virtual string CodigoCatalogo { get; set; }
        public virtual string Nombre { get; set; }
        public virtual int Stock { get; set; }
    }
}
