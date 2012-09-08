using SIGAPPBOM.Dominio.Comun;

namespace SIGAPPBOM.Dominio.Administracion.Personas
{
    public class Persona : Entidad
    {
        public virtual string Nombre { get; set; }
        public virtual string ApellidoPaterno { get; set; }
        public virtual string ApellidoMaterno { get; set; }
        public virtual int DNI { get; set; }
    }
}
