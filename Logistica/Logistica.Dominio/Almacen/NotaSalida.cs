using System;
using System.Collections.Generic;
using SIGAPPBOM.Logistica.Dominio.Comun;
using SIGAPPBOM.Logistica.Dominio.Pedidos;

namespace SIGAPPBOM.Logistica.Dominio.Almacen
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
