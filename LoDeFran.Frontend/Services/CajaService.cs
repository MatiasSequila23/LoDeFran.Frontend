using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class CajaService
    {
        private readonly HttpClient _http;

        public CajaService(HttpClient http)
        {
            _http = http;
        }

        // Obtener la caja abierta de un usuario específico
        public async Task<CajaViewModel?> ObtenerCajaAbiertaPorUsuarioAsync(int usuarioId)
        {
            return await _http.GetFromJsonAsync<CajaViewModel>($"api/Cajas/abierta/usuario/{usuarioId}");
        }

        // Obtener la caja abierta (sin usuario específico, si aplica)
        public async Task<CajaViewModel?> ObtenerCajaAbiertaAsync()
        {
            return await _http.GetFromJsonAsync<CajaViewModel>("api/Cajas/abierta");
        }

        // Abrir una nueva caja
        public async Task<CajaViewModel?> AbrirCajaAsync(CajaViewModel nuevaCaja)
        {
            var response = await _http.PostAsJsonAsync("api/Cajas/abrir", nuevaCaja);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<CajaViewModel>();

            return null;
        }

        // Cerrar la caja actual
        public async Task<bool> CerrarCajaAsync(int cajaId)
        {
            var response = await _http.PutAsync($"api/Cajas/cerrar/{cajaId}", null);
            return response.IsSuccessStatusCode;
        }

        // Obtener historial de todas las cajas
        public async Task<List<CajaViewModel>> ObtenerHistorialCajasAsync()
        {
            return await _http.GetFromJsonAsync<List<CajaViewModel>>("api/Cajas");
        }

        // Obtener una caja por ID
        public async Task<CajaViewModel?> ObtenerCajaPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<CajaViewModel>($"api/Cajas/{id}");
        }
    }
}
