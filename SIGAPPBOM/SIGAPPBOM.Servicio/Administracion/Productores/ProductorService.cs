using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Dominio.Administracion.Productores;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Productores
{
    public class ProductorService : IProductorService
    {
        public List<string> Errores { get; set; }
        private IRepositorio<Productor> productorRepositorio;
        private IMappingEngine mappingEngine;


        public ProductorService(IRepositorio<Productor> iproductorRepositorio, IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.productorRepositorio = iproductorRepositorio;
            this.mappingEngine = mappingEngine;
            Errores = new List<string>();
        }

        public IList<ProductorViewModel> TraerTodo()
        {
            var productor = productorRepositorio.TraerTodo().ToList();

            return mappingEngine.Map<List<Productor>, List<ProductorViewModel>>(productor);
        }

        public IList<ProductorViewModel> TraerListaPor(string nombre)
        {
            var productores = productorRepositorio.TraerTodo().Where(x => x.Nombre.StartsWith(nombre.ToUpper())).ToList();

            if (productores.Count <= 0)
            {
                Errores.Add("No se encontró coincidencias para el artículo");
                return new List<ProductorViewModel>();
            }
            return mappingEngine.Map<List<Productor>, List<ProductorViewModel>>(productores);
        }
    }

   
}
