using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Dominio.Administracion.RolUsuarios;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.RolUsuarios
{
    public class RolUsuarioService : IRolUsuarioService
    {
        public List<string> Errores { get; set; }
        private IRepositorio<RolUsuario> rolUsuarioRepositorio;
        private IMappingEngine mappingEngine;


        public RolUsuarioService(IRepositorio<RolUsuario> irolUsuarioRepositorio, IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.rolUsuarioRepositorio = irolUsuarioRepositorio;
            this.mappingEngine = mappingEngine;
            Errores = new List<string>();
        }

        public IList<RolUsuarioViewModel> TraerTodo()
        {
            var rolUsuario = rolUsuarioRepositorio.TraerTodo().ToList();

            return mappingEngine.Map<List<RolUsuario>, List<RolUsuarioViewModel>>(rolUsuario);
        }

        
    }

   
}
