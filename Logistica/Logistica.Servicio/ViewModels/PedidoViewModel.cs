using System;
using System.Collections.Generic;
using SIGAPPBOM.Logistica.Servicio.Comun;

namespace SIGAPPBOM.Logistica.Servicio.ViewModels
{
    public class PedidoViewModel
    {
        public int Id { get; set; }
        public string Solicitante { get; set; }
        public string Descripcion { get; set; } 
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaAtencion { get; set; }
        public Estado Estado { get; set; }
        public List<DetallePedidoViewModel> Detalles { get; set; }
    }

    public class DetallePedidoViewModel
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ArticuloId { get; set; }
        public int Item { get; set; }
        public string ArticuloNombre { get; set; }
        public string ArticuloCodigoCatalogo { get; set; }
        public int CantidadSolicitada { get; set; }
        public int CantidadAtendida { get; set; }
    }
}
