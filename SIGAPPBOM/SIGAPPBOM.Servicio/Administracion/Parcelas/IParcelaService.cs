using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Parcelas
{
    public interface IParcelaService
    {
        List<string> Errores { get; set; }
        IList<ParcelaViewModel> TraerListaPor(string nombreParcela);
        IList<ParcelaViewModel> TraerTodo();
        bool Grabar(ParcelaViewModel parcelaViewModel);
        ParcelaViewModel TraerPor(int parcelaId);
    }
    
}
