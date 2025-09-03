namespace LoDeFran.Frontend.Models
{
    public class ProductoVarianteViewModel
    {
        public int IdVariante { get; set; }

        public int IdProducto { get; set; }

        public string Nombre { get; set; } = null!;

        public decimal Precio { get; set; }

        public int? Stock { get; set; }
    }
}
