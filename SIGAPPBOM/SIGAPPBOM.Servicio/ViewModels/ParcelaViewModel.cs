using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using SIGAPPBOM.Servicio.Comun;

namespace SIGAPPBOM.Servicio.ViewModels
{
    public class ParcelaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Sector de la Parcela")]
        public string Sector { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Tamaño de la Parcela")]
        public int Tamaño { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Productor de la Parcela")]
        public int IdProductor { get; set; }
    }
}
