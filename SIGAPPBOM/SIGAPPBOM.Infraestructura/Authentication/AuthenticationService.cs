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

            var menuArticulos = new Menu
            {
                Titulo = "Articulos",
                Controlador = "Articulos",
                Accion = "MostrarArticulos",
                SubMenus = new List<Menu>(),
                Imagen = "icon-tags icon-white"
            };

            var menuProductor = new Menu
            {
                Titulo = "Productores",
                Controlador = "Productor",
                Accion = "MostrarProductor",
                SubMenus = new List<Menu>(),
                Imagen = "icon-user icon-white"
            };
			
			var menuParcelas = new Menu
            {
                Titulo = "Parcelas",
                Controlador = "Parcela",
                Accion = "MostrarParcela",
                SubMenus = new List<Menu>(),
                Imagen = "icon-leaf icon-white"
            };
			
			var menuEmpacadoras = new Menu
            {
                Titulo = "Empacadoras",
                Controlador = "Empacadora",
                Accion = "MostrarEmpacadora",
                SubMenus = new List<Menu>(),
                Imagen = "icon-home icon-white"
            };

			var menuDestinos = new Menu
            {
                Titulo = "Destinos",
                Controlador = "Destino",
                Accion = "MostrarDestino",
                SubMenus = new List<Menu>(),
                Imagen = "icon-flag icon-white"
            };
			
			var menuUsuario = new Menu
            {
                Titulo = "Usuarios",
                Controlador = "Usuario",
                Accion = "MostrarUsuario",
                SubMenus = new List<Menu>(),
                Imagen = "icon-user icon-white"
            };
			/*
			var menuRolUsuarios = new Menu
            {
                Titulo = "RolUsuarios",
                Controlador = "RolUsuario",
                Accion = "MostrarRolUsuario",
                SubMenus = new List<Menu>(),
                Imagen = "icon-home icon-white"
            };
			*/
			var menuRoles = new Menu
            {
                Titulo = "Roles",
                Controlador = "Rol",
                Accion = "MostrarRol",
                SubMenus = new List<Menu>(),
                Imagen = "icon-lock icon-white"
            };

            var menuPersonas = new Menu
            {
                Titulo = "Personas",
                Controlador = "Persona",
                Accion = "MostrarPersona",
                SubMenus = new List<Menu>(),
                Imagen = "icon-user icon-white"
            };

			var menuMenues = new Menu
            {
                Titulo = "Menues",
                Controlador = "Menu",
                Accion = "MostrarMenu",
                SubMenus = new List<Menu>(),
                Imagen = "icon-th-list icon-white"
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
                                   },
                               new Usuario
                               {
                                   Nombre = "admin",
                                   Password = "admin",
							       Menus = new List<Menu> {menuArticulos,menuProductor,menuParcelas,menuEmpacadoras,menuDestinos,menuUsuario,menuRoles,menuMenues,menuPersonas},
                                   Roles = new List<string>{"Administrador"}
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