using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class DiaService
    {
        private readonly HttpClient _http;

        public DiaService(HttpClient http)
        {
            _http = http;
        }

        // Traer todos los métodos de pago
        public async Task<List<DiaViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<DiaViewModel>>("Dias")
                   ?? new List<DiaViewModel>();
        }

        // Opcional: Obtener uno por ID
        public async Task<DiaViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<DiaViewModel>($"Dias/{id}");
        }
    }
}
