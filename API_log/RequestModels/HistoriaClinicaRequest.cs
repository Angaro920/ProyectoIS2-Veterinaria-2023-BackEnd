namespace API_Log.RequestModels
{
    public class HistoriaClinicaRequest
    {
        public int IdHistoriaClinica { get; set; }
        public string Fecha { get; set; }
        public string Diagnostico { get; set; }
        public string OrdenMedica { get; set; }
        public int FkIdEmpleado { get; set; }
    }
}
