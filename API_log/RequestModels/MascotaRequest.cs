using Microsoft.AspNetCore.Mvc;

namespace API_Log.RequestModels
{
    public class MascotaRequest 
    {
        public int IdMascota { get; set; }
        public string Nombre { get; set; }
        public int FkIdCliente { get; set; }
        public int FkIdTipoMascota { get; set; }
        public int FkIdRaza { get; set; }
        public int FkIdEstado { get; set; }
    }
}
