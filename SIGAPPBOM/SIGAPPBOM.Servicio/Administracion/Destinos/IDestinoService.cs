using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Destinos
{
    public interface IDestinoService
    {
        List<string> Errores { get; set; }
        IList<DestinoViewModel> TraerListaPor(string nombreDestino);
        IList<DestinoViewModel> TraerTodo();
        bool Grabar(DestinoViewModel destinoViewModel);
        DestinoViewModel TraerPor(int destinoId);
    }
}
