using AutoMapper;
using SIGAPPBOM.Dominio.Pedidos;

namespace SIGAPPBOM.Servicio.MappingResolvers
{
    public class NumeroItemsPedidoResolver : ValueResolver<Pedido,int>
    {
        protected override int ResolveCore(Pedido source)
        {
            return source.Detalles.Count;
        }
    }
}
