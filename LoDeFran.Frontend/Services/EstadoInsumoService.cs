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
        public async Task<List<EstadoInsumoViewModel>> GetEstadoInsumosAsync()
        {
            return await _http.GetFromJsonAsync<List<EstadoInsumoViewModel>>("EstadosInsumos") ?? new();
        }

        public async Task<EstadoInsumoViewModel?> GetEstadoInsumoByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<EstadoInsumoViewModel>($"EstadosInsumos/{id}");
        }

        public async Task<bool> CrearEstadoInsumoAsync(EstadoInsumoViewModel estadoInsumo)
        {
            var response = await _http.PostAsJsonAsync("EstadosInsumos", estadoInsumo);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarEstadoInsumoAsync(EstadoInsumoViewModel estadoInsumo)
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
