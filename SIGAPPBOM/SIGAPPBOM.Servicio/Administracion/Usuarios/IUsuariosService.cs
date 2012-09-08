using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Usuarios
{
    public interface IUsuarioService
    {
        List<string> Errores { get; set; }
        IList<UsuarioViewModel> TraerListaPor(string nombreProdcutor);
        IList<UsuarioViewModel> TraerTodo();
        bool Grabar(UsuarioViewModel usuarioViewModel);
        UsuarioViewModel TraerPor(int usuarioId);
    }
}
