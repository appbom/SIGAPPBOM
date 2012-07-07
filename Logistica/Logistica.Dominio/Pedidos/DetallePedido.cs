using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.Dominio.Comun;

namespace SIGAPPBOM.Logistica.Dominio.Pedidos
{
    public class DetallePedido : Entidad
    {
        public virtual Articulo Articulo { get; set; }
        public virtual int CantidadSolicitada { get; set; }
        public virtual int CantidadAtendida { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
