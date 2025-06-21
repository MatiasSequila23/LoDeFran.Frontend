using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class PedidoService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "Pedidos";

        public PedidoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PedidoViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<PedidoViewModel>>(BaseUrl) ?? new();
        }

        public async Task<PedidoViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<PedidoViewModel>($"{BaseUrl}/{id}");
        }

        public async Task<PedidoViewModel?> CreateAsync(PedidoViewModel pedido)
        {
            var response = await _http.PostAsJsonAsync(BaseUrl, pedido);
            return await response.Content.ReadFromJsonAsync<PedidoViewModel>();
        }

        public async Task<bool> UpdateAsync(int id, PedidoViewModel pedido)
        {
            var response = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", pedido);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PedidoViewModel?> IniciarPedidoAsync(int idMesa)
        {
            var response = await _http.PostAsJsonAsync($"{BaseUrl}/iniciar", idMesa);
            return await response.Content.ReadFromJsonAsync<PedidoViewModel>();
        }

        public async Task<bool> CambiarEstadoAsync(int id, Enums.EstadoPedido nuevoEstado)
        {
            var response = await _http.PutAsJsonAsync($"{BaseUrl}/{id}/estado", nuevoEstado);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AgregarProductoAsync(int pedidoId, int productoId)
        {
            var response = await _http.PostAsJsonAsync($"{BaseUrl}/{pedidoId}/agregar-producto", productoId);
            return response.IsSuccessStatusCode;
        }
        public async Task<PedidoViewModel?> ObtenerPedidoPorMesaIdAsync(int idMesa)
        {
            var response = await _http.GetFromJsonAsync<PedidoViewModel>($"{BaseUrl}/por-mesa/{idMesa}");
            return response;
        }

    }

    

}
