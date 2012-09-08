using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SIGAPPBOM.Dominio.Administracion.Personas;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Servicio.ViewModels;


namespace SIGAPPBOM.Servicio.Administracion.Personas
{
    public class PersonaService: IPersonaService
    {
        public List<string> Errores { get; set; }
        private IRepositorio<Persona> personaRepositorio;
        private IMappingEngine mappingEngine;

        public PersonaService(IRepositorio<Persona> ipersonaRepositorio, IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.personaRepositorio = ipersonaRepositorio;
            this.mappingEngine = mappingEngine;
            Errores = new List<string>();
        }

        public IList<PersonaViewModel> TraerTodo()
        {
            var persona = personaRepositorio.TraerTodo().ToList();

            return mappingEngine.Map<List<Persona>, List<PersonaViewModel>>(persona);
        }

        public IList<PersonaViewModel> TraerListaPor(string nombre)
        {
            var persona = personaRepositorio.TraerTodo().Where(x => x.Nombre.StartsWith(nombre.ToUpper())).ToList();

            if (persona.Count <= 0)
            {
                Errores.Add("No se encontró coincidencias para la Persona");
                return new List<PersonaViewModel>();
            }
            return mappingEngine.Map<List<Persona>, List<PersonaViewModel>>(persona);
        }

        public bool Grabar(PersonaViewModel personaViewModel)
        {
            try
            {
                Persona persona;
                if (string.IsNullOrEmpty(personaViewModel.Nombre))
                    this.Errores.Add("Ingresar el Nombre de la Persona");

                if (string.IsNullOrEmpty(personaViewModel.ApellidoPaterno))
                    this.Errores.Add("Ingresar Apellido Paterno de la Persona");

                if (string.IsNullOrEmpty(personaViewModel.ApellidoMaterno))
                    this.Errores.Add("Ingresar Apellido Materno de la Persona");

                if (personaViewModel.DNI==0)
                    this.Errores.Add("Ingresar DNI de la Persona");
                else
                {
                    if (personaViewModel.Id == 0)
                        persona = new Persona();
                    else
                        persona = personaRepositorio.Load(personaViewModel.Id);

                    persona.Nombre = personaViewModel.Nombre;
                    persona.ApellidoPaterno = personaViewModel.ApellidoPaterno;
                    persona.ApellidoMaterno = personaViewModel.ApellidoMaterno;
                    persona.DNI = personaViewModel.DNI;

                    
                    if (this.Errores.Count == 0)
                    {
                        personaRepositorio.Guardar(persona);
                        personaViewModel.Id = persona.Id;
                    }
                }


            }
            catch (Exception ex)
            {
                this.Errores.Add(ex.Message);
            }

            return this.Errores.Count == 0;
        }

        public bool Actualizar(PersonaViewModel personaViewModel)
        {
            /*
            try
            {
                var persona = personaRepositorio.BuscarPor(personaViewModel.Id);
                if (persona == null)
                    Errores.Add("Pedido no existe");
                else
                {
                    var detallesActualizar = new List<Persona>();
                    if (string.IsNullOrEmpty(personaViewModel.Nombre))
                        this.Errores.Add("Ingresar el Nombre de la Persona");

                    if (string.IsNullOrEmpty(personaViewModel.ApellidoPaterno))
                        this.Errores.Add("Ingresar Apellido Paterno de la Persona");

                    if (string.IsNullOrEmpty(personaViewModel.ApellidoMaterno))
                        this.Errores.Add("Ingresar Apellido Materno de la Persona");

                    if (personaViewModel.DNI == 0)
                        this.Errores.Add("Ingresar DNI de la Persona");
                    else
                    {

                        foreach (var detalle in personaViewModel.Detalles)
                        {
                            var articulo = articulosRepositorio.BuscarPor(detalle.ArticuloId);
                            if (detalle.CantidadSolicitada <= 0)
                                Errores.Add(string.Format("Item {0}: Ingresar cantidad mayor a cero", detalle.Item));

                            if (articulo == null)
                                Errores.Add(string.Format("Item {0}: Articulo no existe", detalle.Item));

                            if (Errores.Count != 0) continue;

                            var detallePedido = persona.Detalles.ToList().Find(x => x.Articulo.Id == detalle.ArticuloId);
                            if (detallePedido != null)
                                detallePedido.CantidadSolicitada = detalle.CantidadSolicitada;
                            else
                                detallePedido = new DetallePedido
                                {
                                    Id = detalle.Id,
                                    Articulo = articulo,
                                    CantidadSolicitada = detalle.CantidadSolicitada,
                                    Pedido = persona
                                };
                            detallesActualizar.Add(detallePedido);
                        }
                    }
                    if (Errores.Count == 0)
                    {
                        persona.Descripcion = personaViewModel.Descripcion;

                        persona.Detalles.Clear();
                        foreach (var detalle in detallesActualizar)
                            persona.Detalles.Add(detalle);

                        pedidosRepositorio.Guardar(persona);
                    }
                }
            }
            catch (Exception ex)
            {
                Errores.Add(ex.Message);
                throw;
            }
            return Errores.Count == 0;
             * 
             */
            return true;
        }


        public PersonaViewModel TraerPor(int personaId)
        {
            var persona = personaRepositorio.BuscarPor(personaId);

            if (persona != null)
            {
                var personaViewModel = mappingEngine.Map<Persona, PersonaViewModel>(persona);

                return personaViewModel;
            }

            return null;
        }

    }
}
