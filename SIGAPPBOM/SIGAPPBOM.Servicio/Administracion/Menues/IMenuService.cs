using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Menues
{
    public interface IMenuService
    {
        List<string> Errores { get; set; }
        IList<MenuViewModel> TraerListaPor(string nombreMenu);
        IList<MenuViewModel> TraerTodo();
        bool Grabar(MenuViewModel menuViewModel);
        MenuViewModel TraerPor(int menuId);
    }
}
