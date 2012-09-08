using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Logistica.Productores
{
    public interface IProductorService
    {
        List<string> Errores { get; set; }
        IList<ProductorViewModel> TraerListaPor(string nombreProdcutor);
        IList<ProductorViewModel> TraerTodo();
    }
}
