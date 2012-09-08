using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Dominio.Administracion.Usuarios;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        public List<string> Errores { get; set; }
        private IRepositorio<Usuario> usuarioRepositorio;
        private IMappingEngine mappingEngine;


        public UsuarioService(IRepositorio<Usuario> iusuarioRepositorio, IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.usuarioRepositorio = iusuarioRepositorio;
            this.mappingEngine = mappingEngine;
            Errores = new List<string>();
        }

        public IList<UsuarioViewModel> TraerTodo()
        {
            var usuario = usuarioRepositorio.TraerTodo().ToList();

            return mappingEngine.Map<List<Usuario>, List<UsuarioViewModel>>(usuario);
        }

        public IList<UsuarioViewModel> TraerListaPor(string nombre)
        {
            var usuarios = usuarioRepositorio.TraerTodo().Where(x => x.Nombre.StartsWith(nombre.ToUpper())).ToList();

            if (usuarios.Count <= 0)
            {
                Errores.Add("No se encontró coincidencias para el usuario");
                return new List<UsuarioViewModel>();
            }
            return mappingEngine.Map<List<Usuario>, List<UsuarioViewModel>>(usuarios);
        }

        public bool Grabar(UsuarioViewModel usuarioViewModel)
        {
            try
            {
                Usuario usuario;
                if (string.IsNullOrEmpty(usuarioViewModel.Nombre))
                    this.Errores.Add("Ingresar el Nombre del Usuario");

                if (string.IsNullOrEmpty(usuarioViewModel.Password))
                    this.Errores.Add("Ingresar Password del Usuario");
                else
                {
                    if (usuarioViewModel.Id == 0)
                        usuario = new Usuario();
                    else
                        usuario = usuarioRepositorio.Load(usuarioViewModel.Id);

                    usuario.Nombre = usuarioViewModel.Nombre;
                    usuario.Password = usuarioViewModel.Password;


                    if (this.Errores.Count == 0)
                    {
                        usuarioRepositorio.Guardar(usuario);
                        usuarioViewModel.Id = usuario.Id;
                    }
                }


            }
            catch (Exception ex)
            {
                this.Errores.Add(ex.Message);
            }

            return this.Errores.Count == 0;
        }

        public bool Actualizar(UsuarioViewModel usuarioViewModel)
        {

            return true;
        }


        public UsuarioViewModel TraerPor(int usuarioId)
        {
            var usuario = usuarioRepositorio.BuscarPor(usuarioId);

            if (usuario != null)
            {
                var usuarioViewModel = mappingEngine.Map<Usuario, UsuarioViewModel>(usuario);

                return usuarioViewModel;
            }

            return null;
        }
    }


}
