using LoDeFran.Frontend.Models;
using LoDeFran.Frontend.Utlis;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class MesaService
    {
        private readonly HttpClient _http;

        public MesaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<MesaViewModel>> GetMesasAsync()
        {
            return await _http.GetFromJsonAsync<List<MesaViewModel>>("Mesas") ?? new();
        }
        public async Task<MesaViewModel?> GetMesasByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<MesaViewModel>($"Mesas/{id}");
        }
        public async Task CambiarEstadoMesaAsync(int? id, Enums.EstadoMesa nuevoEstado)
        {
            var response = await _http.PutAsJsonAsync($"Mesas/{id}/estado", nuevoEstado);
            response.EnsureSuccessStatusCode();
        }
    }
}
