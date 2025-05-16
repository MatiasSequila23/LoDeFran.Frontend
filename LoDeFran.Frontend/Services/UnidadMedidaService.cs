using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class UnidadMedidaService
    {
        private readonly HttpClient _http;
        public UnidadMedidaService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<UnidadMedidaViewModel>> GetEstadoInsumosAsync()
        {
            return await _http.GetFromJsonAsync<List<UnidadMedidaViewModel>>("UnidadesMedidas") ?? new();
        }

        public async Task<UnidadMedidaViewModel?> GetEstadoInsumoByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<UnidadMedidaViewModel>($"UnidadesMedidas/{id}");
        }

        public async Task<bool> CrearEstadoInsumoAsync(UnidadMedidaViewModel unidadMedida)
        {
            var response = await _http.PostAsJsonAsync("UnidadesMedidas", unidadMedida);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarEstadoInsumoAsync(UnidadMedidaViewModel unidadMedida)
        {
            var response = await _http.PutAsJsonAsync($"UnidadesMedidas/{unidadMedida.Id}", unidadMedida);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarEstadoInsumoAsync(int id)
        {
            var response = await _http.DeleteAsync($"UnidadesMedidas/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
