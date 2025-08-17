using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class SubcategoriaProductoService
    {
        private readonly HttpClient _http;

        public SubcategoriaProductoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<SubcategoriaProductoViewModel>> GetSubCategoriasProductosAsync()
        {
            return await _http.GetFromJsonAsync<List<SubcategoriaProductoViewModel>>("SubcategoriaProducto") ?? new();
        }

    }
}
