using System.Collections.Generic;

namespace SIGAPPBOM.Logistica.Web.Models
{
    public class OperacionInvalidaViewModel
    {
        public string Titulo { get; set; }
        public List<string> Mensajes { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }

        public OperacionInvalidaViewModel()
        {
            Mensajes = new List<string>();
        }
    }
}