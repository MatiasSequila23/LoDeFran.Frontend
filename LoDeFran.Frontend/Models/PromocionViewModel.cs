namespace LoDeFran.Frontend.Models
{
	public class PromocionViewModel
	{
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string? Descripcion { get; set; }

        public decimal? ValorDescuento { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public decimal? MontoMinimo { get; set; }

        public int? EstadoId { get; set; }
        public string? NombreEstado { get; set; }

        public int AplicacionId { get; set; }
        public string? NombreAplicacion { get; set; }

        public int TipoDescuentoId { get; set; }
        public string? NombreTipoDescuento { get; set; }

        public int? TipoPromocionId { get; set; }
        public string? NombreTipoPromocion { get; set; }

        public List<PromocionDiaViewModel> PromocionDia { get; set; } = new();
    }
}
