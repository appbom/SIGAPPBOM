using AutoMapper;
using SIGAPPBOM.Dominio.Logistica.Almacen;
using SIGAPPBOM.Dominio.Logistica.Articulos;
using SIGAPPBOM.Dominio.Logistica.Pedidos;
using SIGAPPBOM.Servicio.MappingResolvers;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Web.Bootstraper
{
    public static class AutoMapperConfiguration
    {
        public static void Start()
        {
            Mapper.CreateMap<Pedido, PedidoViewModel>()
                .ForMember(dto => dto.NumeroItems, opt => opt.ResolveUsing<NumeroItemsPedidoResolver>());


            Mapper.CreateMap<DetallePedido, DetallePedidoViewModel>()
                .ForMember(dto => dto.Item, opt => opt.Ignore());

            Mapper.CreateMap<Articulo, ArticuloViewModel>();
            Mapper.CreateMap<NotaSalida, SalidaViewModel>();
            Mapper.CreateMap<DetalleNotaSalida, DetalleSalidaViewModel>()
                .ForMember(dto => dto.Item, opt => opt.Ignore())
                .ForMember(dto => dto.CantidadPedido,
                           opt => opt.ResolveUsing<CantidadArticulosPedidosResolver>())
                .ForMember(dto => dto.CantidadSalida,
                           opt => opt.MapFrom(detalleSalida => detalleSalida.Cantidad));
        }
    }
}