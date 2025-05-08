namespace LoDeFran.Frontend.Models
{
    public class InsumoProducto
    {
        public int ProductoId { get; set; }
        public int InsumoId { get; set; }
        public string? NombreInsumo { get; set; } // opcional, para mostrar en el frontend
        public decimal? Cantidad { get; set; }
    }
}
