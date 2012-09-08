using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Dominio.Administracion.Empacadoras;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Empacadoras
{
    public class EmpacadoraService : IEmpacadoraService
    {
        public List<string> Errores { get; set; }
        private IRepositorio<Empacadora> empacadoraRepositorio;
        private IMappingEngine mappingEngine;


        public EmpacadoraService(IRepositorio<Empacadora> iempacadoraRepositorio, IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.empacadoraRepositorio = iempacadoraRepositorio;
            this.mappingEngine = mappingEngine;
            Errores = new List<string>();
        }

        public IList<EmpacadoraViewModel> TraerTodo()
        {
            var empacadora = empacadoraRepositorio.TraerTodo().ToList();

            return mappingEngine.Map<List<Empacadora>, List<EmpacadoraViewModel>>(empacadora);
        }

        public IList<EmpacadoraViewModel> TraerListaPor(string nombre)
        {
            var empacadoras = empacadoraRepositorio.TraerTodo().Where(x => x.Nombre.StartsWith(nombre.ToUpper())).ToList();

            if (empacadoras.Count <= 0)
            {
                Errores.Add("No se encontró coincidencias para el Empacadora");
                return new List<EmpacadoraViewModel>();
            }
            return mappingEngine.Map<List<Empacadora>, List<EmpacadoraViewModel>>(empacadoras);
        }

        public bool Grabar(EmpacadoraViewModel empacadoraViewModel)
        {
            try
            {
                Empacadora empacadora;
                if (string.IsNullOrEmpty(empacadoraViewModel.Nombre))
                    this.Errores.Add("Ingresar el Nombre de la Empacadora");
                else
                {
                    if (empacadoraViewModel.Id == 0)
                        empacadora = new Empacadora();
                    else
                        empacadora = empacadoraRepositorio.Load(empacadoraViewModel.Id);

                    empacadora.Nombre = empacadoraViewModel.Nombre;


                    if (this.Errores.Count == 0)
                    {
                        empacadoraRepositorio.Guardar(empacadora);
                        empacadoraViewModel.Id = empacadora.Id;
                    }
                }


            }
            catch (Exception ex)
            {
                this.Errores.Add(ex.Message);
            }

            return this.Errores.Count == 0;
        }

        public bool Actualizar(EmpacadoraViewModel empacadoraViewModel)
        {

            return true;
        }


        public EmpacadoraViewModel TraerPor(int empacadoraId)
        {
            var empacadora = empacadoraRepositorio.BuscarPor(empacadoraId);

            if (empacadora != null)
            {
                var empacadoraViewModel = mappingEngine.Map<Empacadora, EmpacadoraViewModel>(empacadora);

                return empacadoraViewModel;
            }

            return null;
        }
    }


}
