using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class MotivoMovimientoService
    {
        private readonly HttpClient _http;

        public MotivoMovimientoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<MotivoMovimientoViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<MotivoMovimientoViewModel>>("MotivosMovimiento") ?? new();
        }

        public async Task<List<MotivoMovimientoViewModel>> GetPorTipoAsync(string tipo)
        {
            return await _http.GetFromJsonAsync<List<MotivoMovimientoViewModel>>($"MotivosMovimiento/tipo/{tipo}") ?? new();
        }
    }
}
