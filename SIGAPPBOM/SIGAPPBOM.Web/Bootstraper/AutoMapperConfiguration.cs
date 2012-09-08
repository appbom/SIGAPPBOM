using AutoMapper;
using SIGAPPBOM.Dominio.Logistica.Almacen;
using SIGAPPBOM.Dominio.Logistica.Articulos;
using SIGAPPBOM.Dominio.Logistica.Pedidos;
using SIGAPPBOM.Servicio.MappingResolvers;
using SIGAPPBOM.Servicio.ViewModels;
using SIGAPPBOM.Dominio.Administracion.Productores;
using SIGAPPBOM.Dominio.Administracion.Empacadoras;
using SIGAPPBOM.Dominio.Administracion.Destinos;
using SIGAPPBOM.Dominio.Administracion.Parcelas;
using SIGAPPBOM.Dominio.Administracion.Usuarios;
using SIGAPPBOM.Dominio.Administracion.Roles;
using SIGAPPBOM.Dominio.Administracion.RolUsuarios;
using SIGAPPBOM.Dominio.Administracion.Menues;
using SIGAPPBOM.Dominio.Administracion.Personas;

namespace SIGAPPBOM.Web.Bootstraper
{
    public static class AutoMapperConfiguration
    {
        public static void Start()
        {
            Mapper.CreateMap<Pedido, PedidoViewModel>()
                .ForMember(dto => dto.NumeroItems, opt => opt.ResolveUsing<NumeroItemsPedidoResolver>());

            Mapper.CreateMap<Productor, ProductorViewModel>();
			Mapper.CreateMap<Destino, DestinoViewModel>();
			Mapper.CreateMap<Empacadora, EmpacadoraViewModel>();
			Mapper.CreateMap<Parcela, ParcelaViewModel>();
			Mapper.CreateMap<Menu, MenuViewModel>();
			Mapper.CreateMap<Usuario, UsuarioViewModel>();
			Mapper.CreateMap<RolUsuario, RolUsuarioViewModel>();
			Mapper.CreateMap<Rol, RolViewModel>();
            Mapper.CreateMap<Persona, PersonaViewModel>();
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