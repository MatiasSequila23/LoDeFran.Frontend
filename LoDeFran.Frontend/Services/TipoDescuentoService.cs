using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class TipoDescuentoService
    {
        private readonly HttpClient _http;

        public TipoDescuentoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TipoDescuentoViewModel>> GetTiposDescuentoAsync()
        {
            return await _http.GetFromJsonAsync<List<TipoDescuentoViewModel>>("TipoDescuento");
        }
    }
}
