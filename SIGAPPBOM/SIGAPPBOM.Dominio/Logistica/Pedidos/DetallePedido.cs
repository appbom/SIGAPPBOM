using SIGAPPBOM.Dominio.Comun;
using SIGAPPBOM.Dominio.Logistica.Articulos;

namespace SIGAPPBOM.Dominio.Logistica.Pedidos
{
    public class DetallePedido : Entidad
    {
        public virtual Articulo Articulo { get; set; }
        public virtual int CantidadSolicitada { get; set; }
        public virtual int CantidadAtendida { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
