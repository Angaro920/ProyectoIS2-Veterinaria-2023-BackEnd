using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.ResponseModels
{
    public class EmailResponseModel
    {
        public int Respuesta { get; set; }
        public String Mensaje { get; set; } = String.Empty;
    }
}
