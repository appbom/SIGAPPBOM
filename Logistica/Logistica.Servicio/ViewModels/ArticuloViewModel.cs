using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGAPPBOM.Logistica.Servicio.ViewModels
{
    public class ArticuloViewModel
    {
        public int Id { get; set; }

        public string CodigoCatalogo { get; set; }

        public string Nombre { get; set; }

        public int Stock { get; set; }
    }
}
