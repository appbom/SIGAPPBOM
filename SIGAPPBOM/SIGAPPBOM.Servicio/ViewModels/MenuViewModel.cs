using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using SIGAPPBOM.Servicio.Comun;

namespace SIGAPPBOM.Servicio.ViewModels
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Titulo del Menu")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Controlador del Menu")]
        public string Controlador { get; set; }

        [Required(ErrorMessage = "Debe ingresar la Accion del Menu")]
        public string Accion { get; set; }

        [Required(ErrorMessage = "Debe ingresar la Imagen del Menu")]
        public string Imagen { get; set; }
    }
}
