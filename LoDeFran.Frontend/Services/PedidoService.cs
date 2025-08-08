using LoDeFran.Frontend.Models;
using LoDeFran.Frontend.Utlis.ClassAux;
using LoDeFran.Frontend.Utlis;
using System.Net.Http;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class PedidoService
    {
        private readonly HttpClient _http;
        public PedidoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PedidoViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<PedidoViewModel>>("Pedidos") ?? new();
        }

        public async Task<PedidoViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<PedidoViewModel>($"Pedidos/{id}");
        }

        public async Task<PedidoViewModel?> CreateAsync(PedidoViewModel pedido)
        {
            var response = await _http.PostAsJsonAsync($"Pedidos/NuevoPedido", pedido);
            return await response.Content.ReadFromJsonAsync<PedidoViewModel>();
        }

        public async Task<bool> UpdateAsync(int id, PedidoViewModel pedido)
        {
            var response = await _http.PutAsJsonAsync($"Pedidos/{id}", pedido);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"Pedidos/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PedidoViewModel?> IniciarPedidoAsync(int idMesa)
        {
           var response = await _http.PostAsJsonAsync($"Pedidos/iniciar", idMesa);
            return await response.Content.ReadFromJsonAsync<PedidoViewModel>();
        }

        public async Task<bool> CambiarEstadoAsync(int id, Enums.EstadoPedido nuevoEstado)
        {
            var response = await _http.PutAsJsonAsync($"Pedidos/{id}/estado", nuevoEstado);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AgregarProductoAsync(int pedidoId, int productoId)
        {
            var response = await _http.PostAsJsonAsync($"Pedidos/{pedidoId}/agregar-producto", productoId);
            return response.IsSuccessStatusCode;
        }
        public async Task<PedidoViewModel?> ObtenerPedidoPorMesaIdAsync(int idMesa)
        {
            var response = await _http.GetFromJsonAsync<PedidoViewModel>($"Pedidos/por-mesa/{idMesa}");
            return response;
        }
        public async Task AgregarComboAlPedidoAsync(int pedidoId, int comboId, int cantidad)
        {
            var request = new AgregarComboRequest
            {
                ComboId = comboId,
                Cantidad = cantidad
            };

            var response = await _http.PostAsJsonAsync($"Pedidos/{pedidoId}/agregar-combo", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al agregar combo: {error}");
            }
        }

        public async Task<PedidoViewModel> CrearPedidoPorClienteMesaAsync(int? clienteId, int? tipoPedidoId, int? mesaId)
        {
            var dtoPedidoMesa = new
            {
                ClienteId = clienteId,
                TipoPedidoId = tipoPedidoId,
                MesaId = mesaId
            };

            var response = await _http.PostAsJsonAsync("Pedidos/crear_pedido_por_cliente_Mesa", dtoPedidoMesa);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PedidoViewModel>()
                   ?? throw new Exception("La API no devolvió un pedido válido.");
        }

        public async Task<PedidoViewModel> CrearPedidoGenericoAsync(CrearPedidoRequest pedidoRequest)
        {
            var response = await _http.PostAsJsonAsync("Pedidos/crear_pedido_generico", pedidoRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PedidoViewModel>()
                   ?? throw new Exception("La API no devolvió un pedido válido.");
        }
        public class AgregarComboRequest
        {
            public int ComboId { get; set; }
            public int Cantidad { get; set; }
        }

    }
}
