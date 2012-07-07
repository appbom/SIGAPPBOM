using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Logistica.Dominio.Comun;

namespace SIGAPPBOM.Logistica.Dominio.Articulos
{
    public class Articulo : Entidad
    {
        public virtual string CodigoCatalogo { get; set; }
        public virtual string Nombre { get; set; }
        public virtual int Stock { get; set; }
    }
}
