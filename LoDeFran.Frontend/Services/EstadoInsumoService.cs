using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class EstadoInsumoService
    {
        private readonly HttpClient _http;
        public EstadoInsumoService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<EstadoInsumo>> GetEstadoInsumosAsync()
        {
            return await _http.GetFromJsonAsync<List<EstadoInsumo>>("EstadosInsumos") ?? new();
        }

        public async Task<EstadoInsumo?> GetEstadoInsumoByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<EstadoInsumo>($"EstadosInsumos/{id}");
        }

        public async Task<bool> CrearEstadoInsumoAsync(EstadoInsumo estadoInsumo)
        {
            var response = await _http.PostAsJsonAsync("EstadosInsumos", estadoInsumo);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarEstadoInsumoAsync(EstadoInsumo estadoInsumo)
        {
            var response = await _http.PutAsJsonAsync($"EstadosInsumos/{estadoInsumo.Id}", estadoInsumo);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarEstadoInsumoAsync(int id)
        {
            var response = await _http.DeleteAsync($"EstadosInsumos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
