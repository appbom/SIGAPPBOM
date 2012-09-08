using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using SIGAPPBOM.Servicio.Comun;

namespace SIGAPPBOM.Servicio.ViewModels
{
    public class EmpacadoraViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Nombre de la Empacadora")]
        public string Nombre { get; set; }
    }
}
