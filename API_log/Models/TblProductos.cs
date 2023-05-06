namespace API_Log.Models
{
    public partial class TblProductos
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int Exitencias { get; set; }
    }
}
