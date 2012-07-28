﻿using AutoMapper;
using SIGAPPBOM.Logistica.Dominio.Articulos;
using SIGAPPBOM.Logistica.Dominio.Pedidos;
using SIGAPPBOM.Logistica.Servicio.MappingResolvers;
using SIGAPPBOM.Logistica.Servicio.ViewModels;

namespace SIGAPPBOM.Logistica.Web.Bootstraper
{
    public static class AutoMapperConfiguration
    {
        public static void Start()
        {
            Mapper.CreateMap<Pedido, PedidoViewModel>()
                .ForMember(dto => dto.NumeroItems, opt => opt.ResolveUsing<NumeroItemsPedidoResolver>());
            Mapper.CreateMap<Articulo, ArticuloViewModel>();

            Mapper.CreateMap<DetallePedido, DetallePedidoViewModel>()
                .ForMember(dto => dto.Item, opt => opt.Ignore());


        }
    }
}