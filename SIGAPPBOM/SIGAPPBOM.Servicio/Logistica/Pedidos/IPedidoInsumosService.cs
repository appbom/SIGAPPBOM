using System.Collections.Generic;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Logistica.Pedidos
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
