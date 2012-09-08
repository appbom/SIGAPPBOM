using SIGAPPBOM.Dominio.Comun;

namespace SIGAPPBOM.Dominio.Administracion.Parcelas
{
    public class Parcela : Entidad
    {
        public virtual string Sector { get; set; }
        public virtual int Tamaño { get; set; }
        public virtual int IdProductor { get; set; }
    }
}
