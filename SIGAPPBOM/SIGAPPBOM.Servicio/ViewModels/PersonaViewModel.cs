using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using SIGAPPBOM.Servicio.Comun;

namespace SIGAPPBOM.Servicio.ViewModels
{
    public class PersonaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Nombre de la Persona")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Debe ingresar el DNI")]
        public int DNI { get; set; }
    }
}
