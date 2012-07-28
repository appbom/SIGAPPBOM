using System.Collections.Generic;
using SIGAPPBOM.Logistica.Servicio.ViewModels;

namespace SIGAPPBOM.Logistica.Servicio.Articulos
{
    public interface IArticuloService
    {
        List<string> Errores { get; set; }
        IList<ArticuloViewModel> TraerListaPor(string nombreArticulo);
    }
}
