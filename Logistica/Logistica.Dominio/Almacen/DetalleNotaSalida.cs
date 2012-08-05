using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.Dominio.Comun;

namespace SIGAPPBOM.Logistica.Dominio.Almacen
{
    public class DetalleNotaSalida:Entidad
    {
        public virtual NotaSalida NotaSalida { get; set; }
        public virtual Articulo Articulo { get; set; }
        public virtual int Cantidad { get; set; }
    }
}
