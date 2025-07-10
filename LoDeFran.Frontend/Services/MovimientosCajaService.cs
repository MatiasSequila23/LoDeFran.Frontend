using LoDeFran.Frontend.Models;
using LoDeFran.Frontend.Pages.Producto;
using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;


namespace LoDeFran.Frontend.Services
{
    public class MovimientosCajaService
    {
        private readonly HttpClient _http;

        public MovimientosCajaService(HttpClient http)
        {
            _http = http;
        }

        // Obtener movimientos por caja
        public async Task<List<MovimientoCajaViewModel>> GetPorCajaAsync(int cajaId)
        {
            return await _http.GetFromJsonAsync<List<MovimientoCajaViewModel>>($"MovimientosCaja/caja/{cajaId}")
                   ?? new List<MovimientoCajaViewModel>();
        }

        // Obtener un solo movimiento por ID
        public async Task<MovimientoCajaViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<MovimientoCajaViewModel>($"MovimientosCaja/{id}");
        }

        // Crear nuevo movimiento
        public async Task<bool>  CrearAsync(MovimientoCajaViewModel movimiento)
        {
            var response = await _http.PostAsJsonAsync("MovimientosCaja", movimiento);
            return response.IsSuccessStatusCode;
        }

        // Actualizar movimiento
        public async Task ActualizarAsync(int id, MovimientoCajaViewModel movimiento)
        {
            await _http.PutAsJsonAsync($"MovimientosCaja/{id}", movimiento);
        }

        // Eliminar movimiento
        public async Task EliminarAsync(int id)
        {
            await _http.DeleteAsync($"MovimientosCaja/{id}");
        }
        public async Task<List<MovimientoCajaViewModel>> GetTodosAsync()
        {
            return await _http.GetFromJsonAsync<List<MovimientoCajaViewModel>>("MovimientosCaja") ?? new List<MovimientoCajaViewModel>();
        }
        public async Task<List<MovimientoCajaViewModel>> GetFiltradosAsync( int? cajaId, int? metodoPagoId, int? motivoId, DateTime? desde, DateTime? hasta)
        {
            var query = new Dictionary<string, string>();
            if (cajaId.HasValue) query.Add("cajaId", cajaId.Value.ToString());
            if (metodoPagoId.HasValue) query.Add("metodoPagoId", metodoPagoId.Value.ToString());
            if (motivoId.HasValue) query.Add("motivoId", motivoId.Value.ToString());
            if (desde.HasValue) query.Add("desde", desde.Value.ToString("yyyy-MM-dd"));
            if (hasta.HasValue) query.Add("hasta", hasta.Value.ToString("yyyy-MM-dd"));

            var url = QueryHelpers.AddQueryString("MovimientosCaja/filtrar", query);
            return await _http.GetFromJsonAsync<List<MovimientoCajaViewModel>>(url) ?? new List<MovimientoCajaViewModel>();
        }


    }
}
