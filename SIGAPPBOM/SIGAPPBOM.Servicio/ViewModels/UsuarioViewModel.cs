using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using SIGAPPBOM.Servicio.Comun;

namespace SIGAPPBOM.Servicio.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Nombre del Usuario")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Password del Usuario")]
        public string Password { get; set; }
    }
}
