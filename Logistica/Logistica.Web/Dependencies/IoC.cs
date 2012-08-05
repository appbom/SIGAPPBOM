using System.Web.Mvc;
using AutoMapper;
using NHibernate;
using SIGAPPBOM.Logistica.Dominio.Pedidos;
using SIGAPPBOM.Logistica.Infraestructura.Authentication;
using SIGAPPBOM.Logistica.Infraestructura.UnitOfWork;
using SIGAPPBOM.Logistica.NHibernate.Repositorios;
using SIGAPPBOM.Logistica.Servicio;
using SIGAPPBOM.Logistica.Servicio.Pedidos;
using StructureMap;

namespace SIGAPPBOM.Logistica.Web.Dependencies
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x=>
                                         {
                                             x.For<ISessionFactory>()
                                                 .Singleton()
                                                 .Add(ServicioNHibernate.SessionFactory());

                                             x.For<IUnitOfWork>()
                                                 .HybridHttpOrThreadLocalScoped()
                                                 .Use<UnitOfWork>();
                                             
                                             x.For(typeof(IRepositorio<>))
                                                .Use(typeof(Repositorio<>));
                                             x.For<IAuthenticationService>().Use<AuthenticationService>();
                                             x.For<IMappingEngine>().Use(Mapper.Engine);
                                             x.Scan(y =>
                                                        {
                                                            y.AssemblyContainingType<Pedido>();
                                                            y.AssemblyContainingType<IPedidoInsumosService>();
                                                            y.AddAllTypesOf<IController>();
                                                            y.TheCallingAssembly();
                                                            y.WithDefaultConventions();
                                                        });
                                         });
            return ObjectFactory.Container;
            
        }
    }
}