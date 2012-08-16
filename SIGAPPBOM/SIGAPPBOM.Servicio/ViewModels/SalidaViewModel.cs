using System;
using System.Collections.Generic;
using SIGAPPBOM.Servicio.Comun;

namespace SIGAPPBOM.Servicio.ViewModels
{
    public class SalidaViewModel
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Estado PedidoEstado { get; set; }
        public string PedidoSolicitante { get; set; }
        public string PedidoDescripcion { get; set; }
        public string Observacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public List<DetalleSalidaViewModel> Detalles { get; set; }

        public SalidaViewModel()
        {
            Detalles = new List<DetalleSalidaViewModel>();
        }
    }
    public class DetalleSalidaViewModel
    {
        public int Item { get; set; }
        public int ArticuloId { get; set; }
        public String ArticuloNombre { get; set; }
        public int CantidadPedido { get; set; }
        public int CantidadSalida { get; set; }
    }
}
