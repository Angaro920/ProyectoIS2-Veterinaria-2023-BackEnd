namespace API_Log.RequestModels
{
    public class ClienteHistoriaMascotaRequest
    {
        public int IdClienteHistoriaMascota { get; set; }
        public int FkIdCliente { get; set; }
        public int FkIdMascota { get; set; }
        public int FkIdHistoria { get; set; }
    }
}
