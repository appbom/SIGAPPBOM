using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Logistica.Servicio.ViewModels;

namespace SIGAPPBOM.Logistica.Servicio.Pedidos
{
    public interface IPedidoBienesService
    {
        List<string> Errores { get; set; }
        IList<PedidoViewModel> TraerLista();
    }

}
