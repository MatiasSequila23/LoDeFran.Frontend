using LoDeFran.Frontend.Models;
using LoDeFran.Frontend.Pages.Insumo;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class InsumoService
    {
        private readonly HttpClient _http;

        public InsumoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Insumo>> GetInsumosAsync()
        {
            return await _http.GetFromJsonAsync<List<Insumo>>("Insumos") ?? new();
        }

        public async Task<Insumo?> GetProductoByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<Insumo>($"Insumos/{id}");
        }

        public async Task<bool> CrearInsumoAsync(Insumo insumos)
        {
            var response = await _http.PostAsJsonAsync("Insumos", insumos);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarInsumoAsync(Insumo insumos)
        {
            var response = await _http.PutAsJsonAsync($"Insumos/{insumos.Id}", insumos);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarInsumoAsync(int id)
        {
            var response = await _http.DeleteAsync($"Insumos/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<List<Proveedor>> GetProveedoresAsync()
        {
            return await _http.GetFromJsonAsync<List<Proveedor>>("Proveedores") ?? new();
        }
        public async Task<List<EstadoInsumo>> GetEstadosAsync()
        {
            return await _http.GetFromJsonAsync<List<EstadoInsumo>>("EstadosInsumos") ?? new();
        }
        public async Task<List<UnidadMedidaViewModel>> GetUnidadesMedidaAsync()
        {
            return await _http.GetFromJsonAsync<List<UnidadMedidaViewModel>>("UnidadesMedidas") ?? new();
        }
        public async Task<bool> RecalcularPreciosPorInsumoAsync(int insumoId)
        {
            var response = await _http.PutAsync($"Insumos/{insumoId}/recalcular-precios-productos", null);
            return response.IsSuccessStatusCode;
        }

    }
}
