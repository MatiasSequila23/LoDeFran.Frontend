using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class CategoriaProductoService
    {
        private readonly HttpClient _http;

        public CategoriaProductoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CategoriaProductoViewModel>> GetCategoriasProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<CategoriaProductoViewModel>>("CategoriasProductos") ?? new();
        }
        
        public async Task<ProductoViewModel?> GetProductoByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ProductoViewModel>($"Productos/{id}");
        }

        public async Task<bool> CrearProductoAsync(ProductoViewModel producto)
        {
            var response = await _http.PostAsJsonAsync("Productos", producto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarProductoAsync(ProductoViewModel producto)
        {
            var response = await _http.PutAsJsonAsync($"Productos/{producto.Id}", producto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarProductoAsync(int id)
        {
            var response = await _http.DeleteAsync($"Productos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
