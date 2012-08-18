using SIGAPPBOM.Dominio.Comun;
using SIGAPPBOM.Dominio.Logistica.Articulos;

namespace SIGAPPBOM.Dominio.Logistica.Almacen
{
    public class DetalleNotaSalida:Entidad
    {
        public virtual NotaSalida NotaSalida { get; set; }
        public virtual Articulo Articulo { get; set; }
        public virtual int Cantidad { get; set; }
    }
}
