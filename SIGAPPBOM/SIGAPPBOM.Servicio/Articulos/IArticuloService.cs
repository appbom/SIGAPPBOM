using System.Collections.Generic;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Articulos
{
    public interface IArticuloService
    {
        List<string> Errores { get; set; }
        IList<ArticuloViewModel> TraerListaPor(string nombreArticulo);
    }
}
