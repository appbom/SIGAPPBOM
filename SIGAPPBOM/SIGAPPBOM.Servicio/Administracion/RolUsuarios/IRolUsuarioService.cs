using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.RolUsuarios
{
    public interface IRolUsuarioService
    {
        List<string> Errores { get; set; }
        IList<RolUsuarioViewModel> TraerTodo();
    }
}
