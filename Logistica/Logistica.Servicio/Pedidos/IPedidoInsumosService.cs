using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Logistica.Servicio.ViewModels;

namespace SIGAPPBOM.Logistica.Servicio.Pedidos
{
    public interface IPedidoInsumosService
    {
        List<string> Errores { get; set; }
        IList<PedidoViewModel> TraerLista();
        PedidoViewModel TraerPor(int p);
        bool Grabar(PedidoViewModel pedidoViewModel);
        bool Actualizar(PedidoViewModel pedidoViewModel);
        bool Eliminar(int pedidoId);
    }

}
