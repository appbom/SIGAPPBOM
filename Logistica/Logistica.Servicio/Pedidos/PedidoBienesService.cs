using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.Dominio.Pedidos;
using SIGAPPBOM.Logistica.NHibernate.Repositorios;
using SIGAPPBOM.Logistica.Servicio.ViewModels;
using AutoMapper;
using StructureMap;

namespace SIGAPPBOM.Logistica.Servicio.Pedidos
{
    public class PedidoBienesService : IPedidoBienesService
    {
        private IRepositorio<Pedido> pedidosRepositorio;
        private IRepositorio<Articulo> articulosRepositorio;
        private IMappingEngine mappingEngine;
        public List<string> Errores { get; set; }

        public PedidoBienesService(IRepositorio<Pedido> pedidosRepositorio,
                                   IRepositorio<Articulo> articulosRepositorio,
                                   IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.pedidosRepositorio = pedidosRepositorio;
            this.articulosRepositorio = articulosRepositorio;
            this.mappingEngine = mappingEngine;
            this.Errores = new List<string>();
        }

        public IList<PedidoViewModel> TraerLista()
        {
            var pedidos = pedidosRepositorio.TraerTodo().ToList();
            if (pedidos.Count <= 0)
            {
                Errores.Add("No hay pedidos de bienes registrados");
                return new List<PedidoViewModel>();
            }
            return mappingEngine.Map<List<Pedido>, List<PedidoViewModel>>(pedidos);
        }
    }
}
