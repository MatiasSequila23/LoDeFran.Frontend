using System.Net.Http.Json;
using LoDeFran.Frontend.Models;
namespace LoDeFran.Frontend.Services
{
    public class ServicioDeMesaService
    {
        private readonly HttpClient _http;

        public ServicioDeMesaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PisoViewModel>> GetPisosAsync()
        {
            return await _http.GetFromJsonAsync<List<PisoViewModel>>("Pisos") ?? new();
        }

        public async Task<List<MesaViewModel>> GetMesasPorPisoAsync(int pisoId)
        {
            return await _http.GetFromJsonAsync<List<MesaViewModel>>($"Pisos/{pisoId}/mesas") ?? new();
        }
        public async Task<PedidoViewModel> IniciarPedidoAsync(int idMesa)
        {
            var pedido = await _http.PostAsJsonAsync("Pedidos/iniciar", idMesa);
            if (pedido.IsSuccessStatusCode)
            {
                return await pedido.Content.ReadFromJsonAsync<PedidoViewModel>();
            }

            throw new Exception("Error al iniciar pedido");
        }
        public async Task CambiarEstadoPedidoAsync(int pedidoId, EstadoPedido nuevoEstado)
        {
            var response = await _http.PutAsJsonAsync($"Pedidos/{pedidoId}/estado", nuevoEstado);
            response.EnsureSuccessStatusCode();
        }
        public async Task<List<ProductoViewModel>> GetProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<ProductoViewModel>>("Productos") ?? new();
        }

        public async Task AgregarProductoAlPedidoAsync(int pedidoId, int productoId)
        {
            var response = await _http.PostAsJsonAsync($"Pedidos/{pedidoId}/agregar-producto", productoId);
            response.EnsureSuccessStatusCode();
        }
        public async Task<PedidoViewModel?> ObtenerPedidoPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<PedidoViewModel>($"Pedidos/{id}");
        }
        // DELETE: Eliminar un detalle por ID y Pedido
        public async Task<bool> DeleteAsync(int idPedido, int idDetalle)
        {
            var response = await _http.DeleteAsync($"Pedidos/{idPedido}/detalle/{idDetalle}");
            return response.IsSuccessStatusCode;
        }
        public async Task<PedidoViewModel?> ObtenerPedidoPorMesaIdAsync(int idMesa)
        {
            var response = await _http.GetFromJsonAsync<PedidoViewModel>($"Pedidos/por-mesa/{idMesa}");
            return response;
        }
        public async Task<List<CategoriaProductoViewModel>> GetCategoriasProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<CategoriaProductoViewModel>>("CategoriasProductos") ?? new();
        }
    }
}
