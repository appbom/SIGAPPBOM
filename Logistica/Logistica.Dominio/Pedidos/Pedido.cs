using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Logistica.Dominio.Comun;

namespace SIGAPPBOM.Logistica.Dominio.Pedidos
{
    public class Pedido : Entidad
    {
        public virtual string Solicitante { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual int Estado { get; set; }
        public virtual DateTime FechaCreacion { get; set; }
        public virtual DateTime? FechaAtencion { get; set; }
        public virtual IList<DetallePedido> Detalles { get; set; }

        public Pedido()
        {
            Detalles = new List<DetallePedido>();
        }

        public virtual void RegistrarDetalle(DetallePedido detallePedido)
        {
            detallePedido.Pedido = this;
            Detalles.Add(detallePedido);
        }
    }
}
