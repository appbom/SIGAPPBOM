using System.Collections.Generic;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Almacen
{
    public interface IAlmacenService
    {
        IList<SalidaViewModel> TraerSalidas();
    }
}
