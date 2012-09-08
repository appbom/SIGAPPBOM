using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Dominio.Administracion.Destinos;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Destinos
{
    public class DestinoService : IDestinoService
    {
        public List<string> Errores { get; set; }
        private IRepositorio<Destino> destinoRepositorio;
        private IMappingEngine mappingEngine;


        public DestinoService(IRepositorio<Destino> idestinoRepositorio, IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.destinoRepositorio = idestinoRepositorio;
            this.mappingEngine = mappingEngine;
            Errores = new List<string>();
        }

        public IList<DestinoViewModel> TraerTodo()
        {
            var destino = destinoRepositorio.TraerTodo().ToList();

            return mappingEngine.Map<List<Destino>, List<DestinoViewModel>>(destino);
        }

        public IList<DestinoViewModel> TraerListaPor(string nombre)
        {
            var destinos = destinoRepositorio.TraerTodo().Where(x => x.Nombre.StartsWith(nombre.ToUpper())).ToList();

            if (destinos.Count <= 0)
            {
                Errores.Add("No se encontró coincidencias para el artículo");
                return new List<DestinoViewModel>();
            }
            return mappingEngine.Map<List<Destino>, List<DestinoViewModel>>(destinos);
        }

        public bool Grabar(DestinoViewModel destinoViewModel)
        {
            try
            {
                Destino destino;
                if (string.IsNullOrEmpty(destinoViewModel.Nombre))
                    this.Errores.Add("Ingresar el Nombre del Destino");
                else
                {
                    if (destinoViewModel.Id == 0)
                        destino = new Destino();
                    else
                        destino = destinoRepositorio.Load(destinoViewModel.Id);

                    destino.Nombre = destinoViewModel.Nombre;


                    if (this.Errores.Count == 0)
                    {
                        destinoRepositorio.Guardar(destino);
                        destinoViewModel.Id = destino.Id;
                    }
                }


            }
            catch (Exception ex)
            {
                this.Errores.Add(ex.Message);
            }

            return this.Errores.Count == 0;
        }

        public bool Actualizar(DestinoViewModel destinoViewModel)
        {

            return true;
        }


        public DestinoViewModel TraerPor(int destinoId)
        {
            var destino = destinoRepositorio.BuscarPor(destinoId);

            if (destino != null)
            {
                var destinoViewModel = mappingEngine.Map<Destino, DestinoViewModel>(destino);

                return destinoViewModel;
            }

            return null;
        }
    }


}
