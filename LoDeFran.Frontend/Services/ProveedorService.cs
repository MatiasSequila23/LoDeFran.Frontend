using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class ProveedorService
    {
        private readonly HttpClient _http;

        public ProveedorService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProveedorViewModel>> GetProveedoresAsync()
        {
            return await _http.GetFromJsonAsync<List<ProveedorViewModel>>("Proveedores") ?? new();
        }

        public async Task<ProveedorViewModel?> GetProductoByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ProveedorViewModel>($"Proveedores/{id}");
        }

        public async Task<bool> CrearProveedorAsync(ProveedorViewModel Proveedores)
        {
            var response = await _http.PostAsJsonAsync("Proveedores", Proveedores);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarProveedorAsync(ProveedorViewModel Proveedores)
        {
            var response = await _http.PutAsJsonAsync($"Proveedores/{Proveedores.Id}", Proveedores);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarProveedorAsync(int id)
        {
            var response = await _http.DeleteAsync($"Proveedores/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
