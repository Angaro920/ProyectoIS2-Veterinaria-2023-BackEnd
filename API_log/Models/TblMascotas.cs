using System.Collections.Generic;

namespace API_Log.Models
{
    public partial class TblMascotas
    {
        public TblMascotas()
        {
            TblClienteHistoriaMascota = new HashSet<TblClienteHistoriaMascota>();
        }

        public int IdMascota { get; set; }
        public string Nombre { get; set; }
        public int FkIdCliente { get; set; }
        public int FkIdTipoMascota { get; set; }
        public int FkIdRaza { get; set; }
        public int FkIdEstado { get; set; }

        public virtual TblCliente FkIdClienteNavigation { get; set; }
        public virtual TblEstados FkIdEstadoNavigation { get; set; }
        public virtual TblRaza FkIdRazaNavigation { get; set; }
        public virtual TblTipoMascota FkIdTipoMascotaNavigation { get; set; }
        public virtual ICollection<TblClienteHistoriaMascota> TblClienteHistoriaMascota { get; set; }
    }
}
