﻿using System.Linq;
using AutoMapper;
using SIGAPPBOM.Logistica.Dominio.Almacen;

namespace SIGAPPBOM.Logistica.Servicio.MappingResolvers
{
    public class CantidadArticulosPedidosResolver : ValueResolver<DetalleNotaSalida, int>
    {
        protected override int ResolveCore(DetalleNotaSalida source)
        {
            return
                source.NotaSalida
                .Pedido.Detalles
                .Single(detalle => detalle.Articulo.Id == source.Articulo.Id)
                .CantidadSolicitada;
        }
    }
}
