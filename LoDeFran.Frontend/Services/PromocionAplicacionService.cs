using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class PromocionAplicacionService
    {
        private readonly HttpClient _http;

        public PromocionAplicacionService(HttpClient http)
        {
            _http = http;
        }

        // Traer todos los métodos de pago
        public async Task<List<PromocionAplicacionViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<PromocionAplicacionViewModel>>("PromocionAplicaciones")
                   ?? new List<PromocionAplicacionViewModel>();
        }

    }
}
