using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SIGAPPBOM.NHibernate.Repositorios;
using SIGAPPBOM.Dominio.Administracion.Parcelas;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Parcelas
{
    public class ParcelaService : IParcelaService
    {
        public List<string> Errores { get; set; }
        private IRepositorio<Parcela> parcelaRepositorio;
        private IMappingEngine mappingEngine;


        public ParcelaService(IRepositorio<Parcela> iparcelaRepositorio, IMappingEngine mappingEngine)
        {
            // TODO: Complete member initialization
            this.parcelaRepositorio = iparcelaRepositorio;
            this.mappingEngine = mappingEngine;
            Errores = new List<string>();
        }

        public IList<ParcelaViewModel> TraerTodo()
        {
            var parcela = parcelaRepositorio.TraerTodo().ToList();

            return mappingEngine.Map<List<Parcela>, List<ParcelaViewModel>>(parcela);
        }

        public IList<ParcelaViewModel> TraerListaPor(string nombre)
        {
            var parcelas = parcelaRepositorio.TraerTodo().Where(x => x.Sector.StartsWith(nombre.ToUpper())).ToList();

            if (parcelas.Count <= 0)
            {
                Errores.Add("No se encontró coincidencias para la Parcela");
                return new List<ParcelaViewModel>();
            }
            return mappingEngine.Map<List<Parcela>, List<ParcelaViewModel>>(parcelas);
        }

        public bool Grabar(ParcelaViewModel parcelaViewModel)
        {
            try
            {
                Parcela parcela;
                if (string.IsNullOrEmpty(parcelaViewModel.Sector))
                    this.Errores.Add("Ingresar el Sector de la Parcela");

                if (parcelaViewModel.Tamaño == 0)
                    this.Errores.Add("Ingresar Tamaño de la Parcela");

                if (parcelaViewModel.IdProductor == 0)
                    this.Errores.Add("Ingresar IdProductor de la Parcela");
                else
                {
                    if (parcelaViewModel.Id == 0)
                        parcela = new Parcela();
                    else
                        parcela = parcelaRepositorio.Load(parcelaViewModel.Id);

                    parcela.Sector = parcelaViewModel.Sector;
                    parcela.Tamaño = parcelaViewModel.Tamaño;
                    parcela.IdProductor = parcelaViewModel.IdProductor;


                    if (this.Errores.Count == 0)
                    {
                        parcelaRepositorio.Guardar(parcela);
                        parcelaViewModel.Id = parcela.Id;
                    }
                }


            }
            catch (Exception ex)
            {
                this.Errores.Add(ex.Message);
            }

            return this.Errores.Count == 0;
        }

        public bool Actualizar(ParcelaViewModel parcelaViewModel)
        {

            return true;
        }


        public ParcelaViewModel TraerPor(int parcelaId)
        {
            var parcela = parcelaRepositorio.BuscarPor(parcelaId);

            if (parcela != null)
            {
                var parcelaViewModel = mappingEngine.Map<Parcela, ParcelaViewModel>(parcela);

                return parcelaViewModel;
            }

            return null;
        }
    }


}
