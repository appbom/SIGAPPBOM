using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.NHibernate.Repositorios;
using SIGAPPBOM.Logistica.Servicio.ViewModels;

namespace SIGAPPBOM.Logistica.Servicio.Articulos
{
    public class ArticuloService : IArticuloService
    {
        public List<string> Errores { get; set; }
        private IRepositorio<Articulo> articuloRepositorio;
        private IMappingEngine mappingEngine;

        public ArticuloService(IRepositorio<Articulo> iArticuloRepositorio, IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.articuloRepositorio = iArticuloRepositorio;
            this.mappingEngine = mappingEngine;
            Errores = new List<string>();
        }

        public IList<ArticuloViewModel> TraerListaPor(string nombre)
        {
            var articulos = articuloRepositorio.TraerTodo().Where(x => x.Nombre.StartsWith(nombre.ToUpper())).ToList();

            if (articulos.Count <= 0)
            {
                Errores.Add("No se encontró coincidencias para el artículo");
                return new List<ArticuloViewModel>();
            }
            return mappingEngine.Map<List<Articulo>, List<ArticuloViewModel>>(articulos); ;
        }
    }
}
