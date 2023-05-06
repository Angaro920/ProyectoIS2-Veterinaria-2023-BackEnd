using System.Collections.Generic;

namespace API_Log.Models
{
    public partial class TblCliente
    {
        public TblCliente()
        {
            TblClienteHistoriaMascota = new HashSet<TblClienteHistoriaMascota>();
            TblMascotas = new HashSet<TblMascotas>();
        }

        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public int Cedula { get; set; }
        public string Correo { get; set; }
        public int Celular { get; set; }
        public int FkIdEstado { get; set; }

        public virtual TblEstados FkIdEstadoNavigation { get; set; }
        public virtual ICollection<TblClienteHistoriaMascota> TblClienteHistoriaMascota { get; set; }
        public virtual ICollection<TblMascotas> TblMascotas { get; set; }
    }
}
