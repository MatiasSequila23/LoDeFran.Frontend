using LoDeFran.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class DetallePedidoServicie
    {
        private readonly HttpClient _http;

        public DetallePedidoServicie(HttpClient http)
        {
            _http = http;
        }
        private const string BaseUrl = "DetallesPedidos";
        // GET: Obtener todos los detalles de pedidos
        public async Task<List<DetallePedidoViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<DetallePedidoViewModel>>(BaseUrl) ?? new();
        }

        // GET: Obtener un detalle por ID
        public async Task<DetallePedidoViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<DetallePedidoViewModel>($"{BaseUrl}/{id}");
        }

        // POST: Crear un nuevo detalle de pedido
        public async Task<DetallePedidoViewModel?> CreateAsync(DetallePedidoViewModel nuevo)
        {
            var response = await _http.PostAsJsonAsync(BaseUrl, nuevo);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<DetallePedidoViewModel>();
            return null;
        }

        // PUT: Actualizar un detalle existente
        public async Task<bool> UpdateAsync(int id, DetallePedidoViewModel actualizado)
        {
            var response = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", actualizado);
            return response.IsSuccessStatusCode;
        }

        // DELETE: Eliminar un detalle por ID y Pedido
        public async Task<bool> DeleteAsync(int idPedido, int idDetalle)
        {
            var response = await _http.DeleteAsync($"pedidos/{idPedido}/detalle/{idDetalle}");
            return response.IsSuccessStatusCode;
        }
    }
}
