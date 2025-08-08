using LoDeFran.Frontend.Models;
using LoDeFran.Frontend.Utlis.Dto;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class FacturaService
    {
        private readonly HttpClient _http;

        public FacturaService(HttpClient http)
        {
            _http = http;
        }

        // Traer todas las facturas
        public async Task<List<FacturaViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<FacturaViewModel>>("Facturas")
                   ?? new List<FacturaViewModel>();
        }

        // Traer una factura por ID
        public async Task<FacturaViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<FacturaViewModel>($"Facturas/{id}");
        }

        // Crear una factura
        public async Task<FacturaViewModel?> CreateAsync(FacturaDto factura)
        {
            var response = await _http.PostAsJsonAsync("Facturas", factura);
            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<FacturaViewModel>()
                : null;
        }

        // Editar una factura
        public async Task<bool> UpdateAsync(int id, FacturaViewModel factura)
        {
            var response = await _http.PutAsJsonAsync($"Facturas/{id}", factura);
            return response.IsSuccessStatusCode;
        }

        // Eliminar una factura
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"Facturas/{id}");
            return response.IsSuccessStatusCode;
        }
        // Filtrar facturas según parámetros opcionales
        public async Task<List<FacturaViewModel>> FiltrarAsync(string? numeroFactura = null, int? estadoId = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            // Construir la query string dinámicamente
            var queryParams = new List<string>();

            if (!string.IsNullOrWhiteSpace(numeroFactura))
                queryParams.Add($"numeroFactura={Uri.EscapeDataString(numeroFactura)}");

            if (estadoId.HasValue)
                queryParams.Add($"estadoId={estadoId.Value}");

            if (fechaDesde.HasValue)
                queryParams.Add($"fechaDesde={fechaDesde.Value:yyyy-MM-dd}");

            if (fechaHasta.HasValue)
                queryParams.Add($"fechaHasta={fechaHasta.Value:yyyy-MM-dd}");

            string url = "Facturas/filtrar";
            if (queryParams.Count > 0)
                url += "?" + string.Join("&", queryParams);

            return await _http.GetFromJsonAsync<List<FacturaViewModel>>(url)
                   ?? new List<FacturaViewModel>();
        }

    }
}
