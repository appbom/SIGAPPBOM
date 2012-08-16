using System.Collections.Generic;

namespace SIGAPPBOM.Infraestructura.Authentication
{

    public class Menu
    {
        public string Titulo { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public string Imagen { get; set; }
        public IList<Menu> SubMenus { get; set; }
    }


}
