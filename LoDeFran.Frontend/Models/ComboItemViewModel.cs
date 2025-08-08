using System.ComponentModel.DataAnnotations;

namespace LoDeFran.Frontend.Models
{
    public class ComboItemViewModel
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }

        public string? NombreProducto { get; set; } // Útil para la vista

        [Range(1, 100)]
        public int Cantidad { get; set; }

        public string? CategoriaGrupo { get; set; }

        public bool EsOpcional { get; set; }
    }
}
