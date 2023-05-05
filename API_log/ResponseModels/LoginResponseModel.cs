using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.ResponseModels
{
    public class LoginResponseModel
    {
        public int Respuesta { get; set; }
        public String Mensaje { get; set; } = String.Empty;
        public String Token { get; set; } = String.Empty;
        public DateTime TiempoExpiracion { get; set; }
    }
}
