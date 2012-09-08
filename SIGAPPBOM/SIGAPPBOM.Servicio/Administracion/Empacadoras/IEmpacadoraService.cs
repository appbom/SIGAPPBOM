using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Empacadoras
{
    public interface IEmpacadoraService
    {
        List<string> Errores { get; set; }
        IList<EmpacadoraViewModel> TraerListaPor(string nombreEmpacadora);
        IList<EmpacadoraViewModel> TraerTodo();
        bool Grabar(EmpacadoraViewModel empacadoraViewModel);
        EmpacadoraViewModel TraerPor(int empacadoraId);
    }
}
