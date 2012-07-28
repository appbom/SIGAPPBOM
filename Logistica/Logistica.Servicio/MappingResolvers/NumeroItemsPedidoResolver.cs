using AutoMapper;
using SIGAPPBOM.Logistica.Dominio.Pedidos;

namespace SIGAPPBOM.Logistica.Servicio.MappingResolvers
{
    public class NumeroItemsPedidoResolver : ValueResolver<Pedido,int>
    {
        protected override int ResolveCore(Pedido source)
        {
            return source.Detalles.Count;
        }
    }
}
