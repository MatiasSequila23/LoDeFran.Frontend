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

        public async Task<List<ProductoViewModel>> GetProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<ProductoViewModel>>("Productos") ?? new();
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
        public async Task<List<CategoriaProductoViewModel>> GetCategoriasProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<CategoriaProductoViewModel>>("CategoriasProductos") ?? new();
        }
        public async Task<List<CategoriaProductoViewModel>> GetSubCategoriasProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<CategoriaProductoViewModel>>("CategoriasProductos") ?? new();
        }
        public async Task<List<EstadoProductoViewModel>> GetEstadosProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<EstadoProductoViewModel>>("EstadosProductos") ?? new();
        }
        public async Task<List<InsumoViewModel>> GetInsumosAsync()
        {
            return await _http.GetFromJsonAsync<List<InsumoViewModel>>("Insumos") ?? new();
        }
        public async Task<bool> AsociarInsumoAProducto(int id, InsumoProductoViewModel insumo)
        {
            // POST a API
            var response = await _http.PostAsJsonAsync($"Productos/{id}/insumos", insumo);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<InsumoProductoViewModel>> GetInsumosAsignadosAsync(int id)
        {
            var response = await _http.GetFromJsonAsync<List<InsumoProductoViewModel>>($"Productos/{id}/insumos") ?? new();
            return response;
        }
        public async Task<bool> ActualizarInsumoProductoAsync(int productoId, int insumoId, InsumoProductoViewModel insumo)
        {
            var response = await _http.PutAsJsonAsync($"Productos/{productoId}/insumos/{insumoId}", insumo);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> EliminarInsumoProductoAsync(int productoId, int insumoId)
        {
            var response = await _http.DeleteAsync($"Productos/{productoId}/insumos/{insumoId}");
            return response.IsSuccessStatusCode;
        }
        public async Task<ProductoViewModel?> RecalcularPrecioProductoAsync(int id)
        {
            // Enviar un PUT con contenido vacío
            var response = await _http.PutAsync($"Productos/{id}/recalcular-precio", new StringContent(""));

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductoViewModel>();
            }

            return null;
        }

    }
}
