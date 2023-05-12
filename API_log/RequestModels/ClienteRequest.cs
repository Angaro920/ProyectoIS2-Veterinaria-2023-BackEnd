namespace API_Log.RequestModels
{
    public class ClienteRequest
    {
        public string Nombre { get; set; }
        public int Cedula { get; set; }
        public string Correo { get; set; }
        public int Celular { get; set; }
        public int FkIdEstado { get; set; }
    }
}
