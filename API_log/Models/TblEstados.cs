using System.Collections.Generic;
using System;

namespace API_Log.Models
{
    public partial class TblEstados
    {
        public TblEstados()
        {
            TblCliente = new HashSet<TblCliente>();
            TblEmpleados = new HashSet<TblEmpleados>();
            TblMascotas = new HashSet<TblMascotas>();
        }

        public int IdEstado { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<TblCliente> TblCliente { get; set; }
        public virtual ICollection<TblEmpleados> TblEmpleados { get; set; }
        public virtual ICollection<TblMascotas> TblMascotas { get; set; }
    }
}
