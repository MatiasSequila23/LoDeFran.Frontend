namespace LoDeFran.Frontend.Models
{
    public class PromocionDiaViewModel
    {
        public int PromocionId { get; set; }

        public int DiaId { get; set; }

        public DateTime? FechaCreacion { get; set; }

        // Opcional: para mostrar en la vista
        public string? DiaNombre { get; set; }

        public string? PromocionNombre { get; set; }
    }
}
