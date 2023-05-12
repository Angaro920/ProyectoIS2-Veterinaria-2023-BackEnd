using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API_Log.RequestModels
{
    public class EmpleadosRequest
    {

        public string Usuario { get; set; }

        public string Contraseña { get; set; }

        public string Rol { get; set; }

        public int FkIdEstado { get; set; }
    }
}
