using System;
using System.Collections.Generic;
using System.Linq;
using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.Dominio.Pedidos;
using SIGAPPBOM.Logistica.NHibernate.Repositorios;
using SIGAPPBOM.Logistica.Servicio.ViewModels;
using AutoMapper;

namespace SIGAPPBOM.Logistica.Servicio.Pedidos
{
    public class PedidoInsumosService : IPedidoInsumosService
    {
        private IRepositorio<Pedido> pedidosRepositorio;
        private IRepositorio<Articulo> articulosRepositorio;
        private IMappingEngine mappingEngine;
        public List<string> Errores { get; set; }

        public PedidoInsumosService(IRepositorio<Pedido> pedidosRepositorio,
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

        public bool Grabar(PedidoViewModel pedidoViewModel)
        {
            try
            {
                Pedido pedido;
                if (string.IsNullOrEmpty(pedidoViewModel.Solicitante))
                    this.Errores.Add("Ingresar solicitante para pedido");

                if (string.IsNullOrEmpty(pedidoViewModel.Descripcion))
                    this.Errores.Add("Ingresar descripción de pedido");

                if (pedidoViewModel.Detalles.Count == 0)
                    this.Errores.Add("Añadir artículos al detalle de pedido");
                else
                {
                    if (pedidoViewModel.Id == 0)
                        pedido = new Pedido();
                    else
                        pedido = pedidosRepositorio.Load(pedidoViewModel.Id);

                    pedido.Solicitante = pedidoViewModel.Solicitante;
                    pedido.Descripcion = pedidoViewModel.Descripcion;
                    pedido.Estado = pedidoViewModel.Estado.GetHashCode();
                    pedido.FechaCreacion = DateTime.Now;

                    foreach (DetallePedidoViewModel detalle in pedidoViewModel.Detalles)
                    {
                        var articulo = articulosRepositorio.BuscarPor(detalle.ArticuloId);
                        if (detalle.CantidadSolicitada <= 0)
                            this.Errores.Add(string.Format("Item {0}: Ingresar cantidad mayor a cero", detalle.Item));

                        if (articulo == null)
                            this.Errores.Add(string.Format("Item {0}: Articulo no existe", detalle.Item));

                        if (this.Errores.Count == 0)
                            pedido.Detalles.Add(new DetallePedido
                            {
                                Id = detalle.Id,
                                Articulo = articulo,
                                CantidadSolicitada = detalle.CantidadSolicitada,
                                Pedido = pedido
                            });
                    }
                    if (this.Errores.Count == 0)
                    {
                        pedidosRepositorio.Guardar(pedido);
                        pedidoViewModel.Id = pedido.Id;
                    }
                }


            }
            catch (Exception ex)
            {
                this.Errores.Add(ex.Message);
            }

            return this.Errores.Count == 0;
        }

        public bool Actualizar(PedidoViewModel pedidoViewModel)
        {
            try
            {
                var pedido = pedidosRepositorio.BuscarPor(pedidoViewModel.Id);
                if (pedido == null)
                    Errores.Add("Pedido no existe");
                else
                {
                    var detallesActualizar = new List<DetallePedido>();
                    if (string.IsNullOrEmpty(pedidoViewModel.Solicitante))
                        Errores.Add("Ingresar solicitante para pedido");

                    if (string.IsNullOrEmpty(pedidoViewModel.Descripcion))
                        Errores.Add("Ingresar descripción de pedido");

                    if (pedidoViewModel.Detalles.Count == 0)
                        Errores.Add("Añadir artículos al detalle de pedido");
                    else
                    {

                        foreach (var detalle in pedidoViewModel.Detalles)
                        {
                            var articulo = articulosRepositorio.BuscarPor(detalle.ArticuloId);
                            if (detalle.CantidadSolicitada <= 0)
                                Errores.Add(string.Format("Item {0}: Ingresar cantidad mayor a cero", detalle.Item));

                            if (articulo == null)
                                Errores.Add(string.Format("Item {0}: Articulo no existe", detalle.Item));

                            if (Errores.Count != 0) continue;

                            var detallePedido = pedido.Detalles.ToList().Find(x => x.Articulo.Id == detalle.ArticuloId);
                            if (detallePedido != null)
                                detallePedido.CantidadSolicitada = detalle.CantidadSolicitada;
                            else
                                detallePedido = new DetallePedido
                                                       {
                                                           Id = detalle.Id,
                                                           Articulo = articulo,
                                                           CantidadSolicitada = detalle.CantidadSolicitada,
                                                           Pedido = pedido
                                                       };
                            detallesActualizar.Add(detallePedido);
                        }
                    }
                    if (Errores.Count == 0)
                    {
                        pedido.Descripcion = pedidoViewModel.Descripcion;

                        pedido.Detalles.Clear();
                        foreach (var detalle in detallesActualizar)
                            pedido.Detalles.Add(detalle);

                        pedidosRepositorio.Guardar(pedido);
                    }
                }
            }
            catch (Exception ex)
            {
                Errores.Add(ex.Message);
                throw;
            }
            return Errores.Count == 0;
        }
    }
}
