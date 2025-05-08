namespace LoDeFran.Frontend.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? CategoriaProductoId { get; set; }
        public string CategoriaProductoNombre { get; set; }
        public int? EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public int? InsumoId { get; set; }
        public string? InsumoNombre { get; set; }
        public bool TieneInsumos { get; set; }
        public List<InsumoProducto>? InsumosProducto { get; set; }
    }
}
