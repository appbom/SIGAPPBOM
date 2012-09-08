using SIGAPPBOM.Dominio.Comun;

namespace SIGAPPBOM.Dominio.Administracion.Productores
{
    public class Productor : Entidad
    {
        public virtual string Nombre { get; set; }
        public virtual int DNI { get; set; }
    }
}
