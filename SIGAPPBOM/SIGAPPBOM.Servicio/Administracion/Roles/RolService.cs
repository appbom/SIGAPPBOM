using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Dominio.Administracion.Roles;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Roles
{
    public class RolService : IRolService
    {
        public List<string> Errores { get; set; }
        private IRepositorio<Rol> rolRepositorio;
        private IMappingEngine mappingEngine;


        public RolService(IRepositorio<Rol> irolRepositorio, IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.rolRepositorio = irolRepositorio;
            this.mappingEngine = mappingEngine;
            Errores = new List<string>();
        }

        public IList<RolViewModel> TraerTodo()
        {
            var rol = rolRepositorio.TraerTodo().ToList();

            return mappingEngine.Map<List<Rol>, List<RolViewModel>>(rol);
        }

        public IList<RolViewModel> TraerListaPor(int rolId)
        {
            var roles = rolRepositorio.TraerTodo().Where(x => x.Id == rolId).ToList();

            if (roles.Count <= 0)
            {
                Errores.Add("No se encontró coincidencias para el Rol");
                return new List<RolViewModel>();
            }
            return mappingEngine.Map<List<Rol>, List<RolViewModel>>(roles);
        }

        public bool Grabar(RolViewModel rolViewModel)
        {
            try
            {
                Rol rol;
                if (string.IsNullOrEmpty(rolViewModel.Nombre))
                    this.Errores.Add("Ingresar el Nombre de la Rol");
                else
                {
                    if (rolViewModel.Id == 0)
                        rol = new Rol();
                    else
                        rol = rolRepositorio.Load(rolViewModel.Id);

                    rol.Nombre = rolViewModel.Nombre;


                    if (this.Errores.Count == 0)
                    {
                        rolRepositorio.Guardar(rol);
                        rolViewModel.Id = rol.Id;
                    }
                }


            }
            catch (Exception ex)
            {
                this.Errores.Add(ex.Message);
            }

            return this.Errores.Count == 0;
        }

        public bool Actualizar(RolViewModel rolViewModel)
        {

            return true;
        }


        public RolViewModel TraerPor(int rolId)
        {
            var rol = rolRepositorio.BuscarPor(rolId);

            if (rol != null)
            {
                var rolViewModel = mappingEngine.Map<Rol, RolViewModel>(rol);

                return rolViewModel;
            }

            return null;
        }

    }


}
