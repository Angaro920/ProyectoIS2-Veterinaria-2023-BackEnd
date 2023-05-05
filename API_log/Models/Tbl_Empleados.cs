using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Log.Models
{
    public class Tbl_Empleados
    {
        [Key]
        public int ID_Empleado { get; set; }

        [Required, NotNull]
        public string Usuario { get; set; }

        [Required, NotNull]
        public string Contraseña { get; set; }

        [Required, NotNull]
        public string Rol { get; set; }

        [Required, NotNull]
        public int FK_ID_Estado { get; set; }

    }
}
