using SIGAPPBOM.Dominio.Articulos;
using SIGAPPBOM.Dominio.Comun;

namespace SIGAPPBOM.Dominio.Almacen
{
    public class DetalleNotaSalida:Entidad
    {
        public virtual NotaSalida NotaSalida { get; set; }
        public virtual Articulo Articulo { get; set; }
        public virtual int Cantidad { get; set; }
    }
}
