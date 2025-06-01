using LoDeFran.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class ProductoService
    {
        private readonly HttpClient _http;

        public ProductoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<Producto>>("Productos") ?? new();
        }

        public async Task<Producto?> GetProductoByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Producto>($"Productos/{id}");
        }

        public async Task<bool> CrearProductoAsync(Producto producto)
        {
            var response = await _http.PostAsJsonAsync("Productos", producto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarProductoAsync(Producto producto)
        {
            var response = await _http.PutAsJsonAsync($"Productos/{producto.Id}", producto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarProductoAsync(int id)
        {
            var response = await _http.DeleteAsync($"Productos/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<List<CategoriaProducto>> GetCategoriasProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<CategoriaProducto>>("CategoriasProductos") ?? new();
        }
        public async Task<List<EstadoProducto>> GetEstadosProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<EstadoProducto>>("EstadosProductos") ?? new();
        }
        public async Task<List<Insumo>> GetInsumosAsync()
        {
            return await _http.GetFromJsonAsync<List<Insumo>>("Insumos") ?? new();
        }
        public async Task<bool> AsociarInsumoAProducto(int id, InsumoProducto insumo)
        {
            // POST a API
            var response = await _http.PostAsJsonAsync($"Productos/{id}/insumos", insumo);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<InsumoProducto>> GetInsumosAsignadosAsync(int id)
        {
            var response = await _http.GetFromJsonAsync<List<InsumoProducto>>($"Productos/{id}/insumos") ?? new();
            return response;
        }
        public async Task<bool> ActualizarInsumoProductoAsync(int productoId, int insumoId, InsumoProducto insumo)
        {
            var response = await _http.PutAsJsonAsync($"Productos/{productoId}/insumos/{insumoId}", insumo);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> EliminarInsumoProductoAsync(int productoId, int insumoId)
        {
            var response = await _http.DeleteAsync($"Productos/{productoId}/insumos/{insumoId}");
            return response.IsSuccessStatusCode;
        }
        public async Task<Producto?> RecalcularPrecioProductoAsync(int id)
        {
            // Enviar un PUT con contenido vacío
            var response = await _http.PutAsync($"Productos/{id}/recalcular-precio", new StringContent(""));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Producto>();
            }

            return null;
        }

    }
}
