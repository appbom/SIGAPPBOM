using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Roles
{
    public interface IRolService
    {
        List<string> Errores { get; set; }
        IList<RolViewModel> TraerListaPor(int RolId);
        IList<RolViewModel> TraerTodo();
        bool Grabar(RolViewModel rolViewModel);
        RolViewModel TraerPor(int RolId);
    }
}
