using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class MetodoPagoService
    {
        private readonly HttpClient _http;

        public MetodoPagoService(HttpClient http)
        {
            _http = http;
        }

        // Traer todos los métodos de pago
        public async Task<List<MetodoPagoViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<MetodoPagoViewModel>>("MetodoPago")
                   ?? new List<MetodoPagoViewModel>();
        }

        // Opcional: Obtener uno por ID
        public async Task<MetodoPagoViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<MetodoPagoViewModel>($"MetodoPago/{id}");
        }
    }
}

