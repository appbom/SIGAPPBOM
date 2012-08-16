using System;
using System.Collections.Generic;
using SIGAPPBOM.Dominio.Comun;
using SIGAPPBOM.Dominio.Pedidos;

namespace SIGAPPBOM.Dominio.Almacen
{
    public class NotaSalida:Entidad
    {
        public virtual Pedido Pedido { get; set; }
        public virtual string Observacion { get; set; }
        public virtual DateTime Fecha { get; set; }
        public virtual string Usuario { get; set; }
        public virtual IList<DetalleNotaSalida> Detalles { get; set; }

        public virtual void RegistrarDetalle(DetalleNotaSalida detalle)
        {
            detalle.NotaSalida = this;
            this.Detalles.Add(detalle);
        }

        public NotaSalida()
        {
            Detalles = new List<DetalleNotaSalida>();
        }
    }
}
