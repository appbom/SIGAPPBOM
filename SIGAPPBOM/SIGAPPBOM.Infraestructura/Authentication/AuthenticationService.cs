using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGAPPBOM.Infraestructura.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private List<Usuario> usuarios;

        public AuthenticationService()
        {
            var menuPedidos = new Menu
            {
                Titulo = "Pedidos",
                Controlador = "PedidosInsumos",
                Accion = "MostrarPedidos",
                SubMenus = new List<Menu>(),
                Imagen = "icon-tasks icon-white"
            };

            var menuAlmacen = new Menu
            {
                Titulo = "Salidas",
                Controlador = "Almacen",
                Accion = "MostrarSalidas",
                SubMenus = new List<Menu>(),
                Imagen = "icon-home icon-white"
            };

            usuarios = new List<Usuario>
                           {
                              new Usuario
                                   {
                                       Nombre = "jrodriguez",
                                       Password = "123456",
                                       Menus = new List<Menu> {menuPedidos,menuAlmacen },
                                       Roles = new List<string>{"AsistenteLogistica"}
                                   },
                              new Usuario
                                   {
                                       Nombre = "agonzales",
                                       Password = "123456",
                                       Menus = new List<Menu> {menuPedidos,menuAlmacen },
                                       Roles = new List<string>{"AsistenteLogistica"}
                                   },
                               new Usuario
                                   {
                                       Nombre = "icampos",
                                       Password = "123456",
									   Menus = new List<Menu> {menuPedidos,menuAlmacen },
                                       Roles = new List<string>{"AsistenteProduccion"}
                                   }
                           };
        }

        public bool ValidaUsuario(string usuario, string password)
        {
            var user = usuarios.SingleOrDefault(x => x.Nombre == usuario && x.Password == password);

            return (user != null);
        }

        public IUserPrincipal ObtienerInformacionUsuario()
        {
            var usuario = new UserIdentity(HttpContext.Current.User.Identity.GetHashCode(),
                               HttpContext.Current.User.Identity.Name);

            var user = usuarios.SingleOrDefault(x => x.Nombre == usuario.Name);
            if (user == null)
                return null;

            var userIdentity = new UserPrincipal(usuario, (IList)user.Roles);
            userIdentity.Menu = user.Menus;
            return userIdentity;
        }

        public IList GetRoles(string usuario)
        {
            var user = usuarios.SingleOrDefault(x => x.Nombre == usuario);
            if (user == null)
                return null;

            return (IList)user.Roles;
        }
    }

    class Usuario
    {
        public string Nombre { get; set; }
        public string Password { get; set; }
        public IList<Menu> Menus { get; set; }
        public IList<string> Roles { get; set; }
    }
}