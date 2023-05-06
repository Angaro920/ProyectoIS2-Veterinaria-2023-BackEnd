namespace API_Log.Models
{
    public partial class TblAuditoria
    {
        public int IdAccion { get; set; }
        public string Accion { get; set; }
        public string Descripcion { get; set; }
        public string FechaHora { get; set; }
        public int IdUsuario { get; set; }
    }
}
