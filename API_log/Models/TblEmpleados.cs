using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Log.Models
{
    public partial class TblEmpleados
    {
            public TblEmpleados()
            {
                TblHistoriaClinica = new HashSet<TblHistoriaClinica>();
            }
            [Key]
            public int IdEmpleado { get; set; }
            [Required, NotNull]
            public string Usuario { get; set; }
            [Required, NotNull]
            public string Contraseña { get; set; }
            [Required, NotNull]
            public string Rol { get; set; }
            [Required, NotNull]
            public int FkIdEstado { get; set; }

            public virtual TblEstados FkIdEstadoNavigation { get; set; }
            public virtual ICollection<TblHistoriaClinica> TblHistoriaClinica { get; set; }
        }
    }

