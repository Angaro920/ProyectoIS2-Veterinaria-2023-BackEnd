using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.RequestModels
{
    public class EmailRequestModel
    {
        public string Destinatario { get; set; }
        public string Asunto { get; set; }
        public string Usuario { get; set; }
    }
}
