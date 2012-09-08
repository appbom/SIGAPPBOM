using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPPBOM.Servicio.ViewModels;

namespace SIGAPPBOM.Servicio.Administracion.Personas
{
    public interface IPersonaService
    {
        List<string> Errores { get; set; }
        IList<PersonaViewModel> TraerListaPor(string nombrePersona);
        IList<PersonaViewModel> TraerTodo();
        bool Grabar(PersonaViewModel personaViewModel);
        PersonaViewModel TraerPor(int personaId);
    }
}
