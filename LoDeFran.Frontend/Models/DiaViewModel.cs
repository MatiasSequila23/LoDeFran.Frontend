namespace LoDeFran.Frontend.Models
{
    public class DiaViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int Codigo { get; set; }

        public bool Seleccionado { get; set; } = false;
    }
}
