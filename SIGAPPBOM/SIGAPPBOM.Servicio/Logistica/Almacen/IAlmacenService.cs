using System.Collections.Generic;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Logistica.Almacen
{
    public interface IAlmacenService
    {
        IList<SalidaViewModel> TraerSalidas();
    }
}
