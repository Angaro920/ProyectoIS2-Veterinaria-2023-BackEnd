namespace API_Log.Models
{
    public partial class TblClienteHistoriaMascota
    {
        public int IdClienteHistoriaMascota { get; set; }
        public int FkIdCliente { get; set; }
        public int FkIdMascota { get; set; }
        public int FkIdHistoria { get; set; }

        public virtual TblCliente FkIdClienteNavigation { get; set; }
        public virtual TblHistoriaClinica FkIdHistoriaNavigation { get; set; }
        public virtual TblMascotas FkIdMascotaNavigation { get; set; }
    }
}
