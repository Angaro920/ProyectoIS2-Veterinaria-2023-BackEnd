using System.Collections.Generic;

namespace API_Log.Models
{
    public partial class TblHistoriaClinica
    {
        public TblHistoriaClinica()
        {
            TblClienteHistoriaMascota = new HashSet<TblClienteHistoriaMascota>();
        }

        public int IdHistoriaClinica { get; set; }
        public string Fecha { get; set; }
        public string Diagnostico { get; set; }
        public string OrdenMedica { get; set; }
        public int FkIdEmpleado { get; set; }

        public virtual TblEmpleados FkIdEmpleadoNavigation { get; set; }
        public virtual ICollection<TblClienteHistoriaMascota> TblClienteHistoriaMascota { get; set; }
    }
}
