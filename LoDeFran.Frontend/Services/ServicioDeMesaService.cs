using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using LoDeFran.Frontend.Models;
using LoDeFran.Frontend.Utlis.ClassAux;
using LoDeFran.Frontend.Utlis;
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
        public async Task<List<TipoPedidoViewModel>> GetTiposPedidoAsync()
        {
            var tipos = await _http.GetFromJsonAsync<List<TipoPedidoViewModel>>("Pedidos/tipos");
            return tipos ?? new();
        }

        public async Task<List<MesaViewModel>> GetMesasPorPisoAsync(int pisoId)
        {
            return await _http.GetFromJsonAsync<List<MesaViewModel>>($"Pisos/{pisoId}/mesas") ?? new();
        }
        public async Task<List<MesaViewModel>> GetMesasAsync()
        {
            return await _http.GetFromJsonAsync<List<MesaViewModel>>("Mesas") ?? new();
        }
        public async Task<MesaViewModel?> GetMesasByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<MesaViewModel>($"Mesas/{id}");
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
        public async Task CambiarEstadoPedidoAsync(int pedidoId,Enums.EstadoPedido nuevoEstado)
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
        public async Task AgregarProductoAlPedidoAsync(int idPedido, int idProducto, int cantidad)
        {
            var dto = new
            {
                ProductoId = idProducto,
                Cantidad = cantidad
            };

            var response = await _http.PostAsJsonAsync($"DetallesPedidos/{idPedido}/agregar-producto", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task<PedidoViewModel?> ObtenerPedidoPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<PedidoViewModel>($"Pedidos/{id}");
        }
        // DELETE: Eliminar un detalle por ID y Pedido
        public async Task<bool> DeleteAsync(int idPedido, int idDetalle)
        {
            var response = await _http.DeleteAsync($"DetallesPedidos/Pedidos/{idPedido}/detalle/{idDetalle}");
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
        public async Task ModificarCantidadProductoAsync(int idDetalle, int nuevaCantidad)
        {
            var response = await _http.PutAsJsonAsync($"DetallesPedidos/detalle/{idDetalle}/cantidad", nuevaCantidad);
            response.EnsureSuccessStatusCode();
        }
        public async Task<List<PedidoViewModel>?> ObtenerPedidoCocina()
        {
            var response = await _http.GetFromJsonAsync<List<PedidoViewModel>>($"Pedidos/cocina");
            return response;
        }
        public async Task<List<PedidoViewModel>> GetPedidosPorEstadoAsync(int idEstado)
        {
            var response = await _http.GetFromJsonAsync<List<PedidoViewModel>>($"Pedidos/por-estado/{idEstado}");
            return response ?? new();
        }
        public async Task CambiarEstadoMesaAsync(int? id, Enums.EstadoMesa nuevoEstado)
        {
            var response = await _http.PutAsJsonAsync($"Mesas/{id}/estado", nuevoEstado);
            response.EnsureSuccessStatusCode();
        }
        public async Task CrearPedidoSinMesaAsync(PedidoViewModel pedido)
        {
            var response = await _http.PostAsJsonAsync("Pedidos", pedido);
            response.EnsureSuccessStatusCode();
        }
        public async Task<ClienteViewModel> CrearClienteAsync(ClienteViewModel nuevoCliente)
        {
            var response = await _http.PostAsJsonAsync("Clientes", nuevoCliente);
            response.EnsureSuccessStatusCode();

            var clienteCreado = await response.Content.ReadFromJsonAsync<ClienteViewModel>();
            return clienteCreado!;
        }
        public async Task<List<ClienteViewModel>> BuscarClientesAsync(string busqueda)
        {
            if (string.IsNullOrWhiteSpace(busqueda))
                return new List<ClienteViewModel>();

            var clientes = await _http.GetFromJsonAsync<List<ClienteViewModel>>($"Clientes/buscar?busqueda={Uri.EscapeDataString(busqueda)}");
            return clientes ?? new List<ClienteViewModel>();
        }
        public async Task<int> CrearPedidoSinMesaYDevolverIdAsync(PedidoViewModel pedido)
        {
            var response = await _http.PostAsJsonAsync("Pedidos/crear-con-id", pedido);
            response.EnsureSuccessStatusCode();

            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }
        public async Task<PedidoViewModel> CrearPedidoAsync(int clienteId, int tipoPedidoId)
        {
            var payload = new CrearPedidoRequest
            {
                ClienteId = clienteId,
                TipoPedidoId = tipoPedidoId
            };

            var response = await _http.PostAsJsonAsync("Pedidos/crear-pedido", payload);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<PedidoViewModel>();

            throw new Exception("No se pudo crear el pedido.");
        }
        public async Task<string> TestClienteAsync(int clienteId)
        {
            var dto = new
            {
                ClienteId = 66,
                TipoPedidoId = 55
            };

            var response = await _http.PostAsJsonAsync($"Pedidos/test-cliente", dto);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }

            throw new Exception("Error al enviar clienteId.");
        }
        public async Task<PedidoViewModel> CrearPedidoPorClienteAsync(int clienteId, int tipoPedidoId)
        {
            var dto = new
            {
                ClienteId = clienteId,
                TipoPedidoId = tipoPedidoId
            };

            var response = await _http.PostAsJsonAsync("Pedidos/crear_pedido_por_cliente", dto);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PedidoViewModel>()
                   ?? throw new Exception("La API no devolvió un pedido válido.");
        }
        public async Task<List<PedidoViewModel>?> PedidosFueraDeSalon()
        {
            var response = await _http.GetFromJsonAsync<List<PedidoViewModel>>($"Pedidos/no-salon");
            return response;
        }
        public async Task<PedidoViewModel> CrearPedidoPorClienteMesaAsync(int clienteId, int tipoPedidoId, int mesaId )
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
    }
}
