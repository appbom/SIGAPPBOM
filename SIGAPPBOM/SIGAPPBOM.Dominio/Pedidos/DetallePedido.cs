using SIGAPPBOM.Dominio.Articulos;
using SIGAPPBOM.Dominio.Comun;

namespace SIGAPPBOM.Dominio.Pedidos
{
    public class DetallePedido : Entidad
    {
        public virtual Articulo Articulo { get; set; }
        public virtual int CantidadSolicitada { get; set; }
        public virtual int CantidadAtendida { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
