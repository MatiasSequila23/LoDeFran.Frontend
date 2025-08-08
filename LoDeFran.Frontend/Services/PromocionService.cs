using LoDeFran.Frontend.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
	public class PromocionService
	{
        private readonly HttpClient _http;

        public PromocionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PromocionViewModel>> GetPromocionesAsync()
		{
			return await _http.GetFromJsonAsync<List<PromocionViewModel>>("Promociones");
		}

		public async Task<PromocionViewModel?> GetPromocionByIdAsync(int id)
		{
			return await _http.GetFromJsonAsync<PromocionViewModel>($"Promociones/{id}");
		}

		public async Task<bool> CreatePromocionAsync(PromocionViewModel promocion)
		{
			var response = await _http.PostAsJsonAsync("Promociones", promocion);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdatePromocionAsync(PromocionViewModel promocion)
		{
			var response = await _http.PutAsJsonAsync($"Promociones/{promocion.Id}", promocion);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeletePromocionAsync(int id)
		{
			var response = await _http.DeleteAsync($"Promociones/{id}");
			return response.IsSuccessStatusCode;
		}
        public async Task<PromocionFormDataViewModel?> GetFormDataAsync()
        {
            var response = await _http.GetFromJsonAsync<PromocionFormDataViewModel>("promociones/form-data");
            return response;
        }
        public async Task<List<PromocionViewModel>> ObtenerPromocionesParaPedidoAsync(int pedidoId)
        {
            var response = await _http.GetFromJsonAsync<List<PromocionViewModel>>($"promociones/promociones_disponibles_para_pedido/{pedidoId}");
            return response ?? new List<PromocionViewModel>();
        }
        public async Task AsignarPromocionAlPedidoAsync(int pedidoId, int promocionId)
        {
            var response = await _http.PutAsJsonAsync($"promociones/{pedidoId}/asignar_promocion", new { PromocionId = promocionId });
            response.EnsureSuccessStatusCode();
        }

    }
}
