using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Dominio.Administracion.Menues;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Menues
{
    public class MenuService : IMenuService
    {
        public List<string> Errores { get; set; }
        private IRepositorio<Menu> menuRepositorio;
        private IMappingEngine mappingEngine;


        public MenuService(IRepositorio<Menu> imenuRepositorio, IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.menuRepositorio = imenuRepositorio;
            this.mappingEngine = mappingEngine;
            Errores = new List<string>();
        }

        public IList<MenuViewModel> TraerTodo()
        {
            var menu = menuRepositorio.TraerTodo().ToList();

            return mappingEngine.Map<List<Menu>, List<MenuViewModel>>(menu);
        }

        public IList<MenuViewModel> TraerListaPor(string nombre)
        {
            var menues = menuRepositorio.TraerTodo().Where(x => x.Titulo.StartsWith(nombre.ToUpper())).ToList();

            if (menues.Count <= 0)
            {
                Errores.Add("No se encontró coincidencias para el Menu");
                return new List<MenuViewModel>();
            }
            return mappingEngine.Map<List<Menu>, List<MenuViewModel>>(menues);
        }

        public bool Grabar(MenuViewModel menuViewModel)
        {
            try
            {
                Menu menu;
                if (string.IsNullOrEmpty(menuViewModel.Titulo))
                    this.Errores.Add("Ingresar el Titulo de la Menu");

                if (string.IsNullOrEmpty(menuViewModel.Controlador))
                    this.Errores.Add("Ingresar Apellido Paterno de la Menu");

                if (string.IsNullOrEmpty(menuViewModel.Accion))
                    this.Errores.Add("Ingresar Apellido Materno de la Menu");

                if (string.IsNullOrEmpty(menuViewModel.Imagen))
                    this.Errores.Add("Ingresar DNI de la Menu");
                else
                {
                    if (menuViewModel.Id == 0)
                        menu = new Menu();
                    else
                        menu = menuRepositorio.Load(menuViewModel.Id);

                    menu.Titulo = menuViewModel.Titulo;
                    menu.Controlador = menuViewModel.Controlador;
                    menu.Accion = menuViewModel.Accion;
                    menu.Imagen = menuViewModel.Imagen;


                    if (this.Errores.Count == 0)
                    {
                        menuRepositorio.Guardar(menu);
                        menuViewModel.Id = menu.Id;
                    }
                }


            }
            catch (Exception ex)
            {
                this.Errores.Add(ex.Message);
            }

            return this.Errores.Count == 0;
        }

        public bool Actualizar(MenuViewModel menuViewModel)
        {

            return true;
        }


        public MenuViewModel TraerPor(int menuId)
        {
            var menu = menuRepositorio.BuscarPor(menuId);

            if (menu != null)
            {
                var menuViewModel = mappingEngine.Map<Menu, MenuViewModel>(menu);

                return menuViewModel;
            }

            return null;
        }
    }


}
