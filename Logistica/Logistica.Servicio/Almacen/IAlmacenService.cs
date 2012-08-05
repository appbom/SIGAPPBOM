using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Logistica.Servicio.ViewModels;

namespace SIGAPPBOM.Logistica.Servicio.Almacen
{
    public interface IAlmacenService
    {
        IList<SalidaViewModel> TraerSalidas();
    }
}
