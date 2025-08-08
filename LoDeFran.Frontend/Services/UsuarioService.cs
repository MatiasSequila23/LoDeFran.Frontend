using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _http;
        public UsuarioService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<UsuarioViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<UsuarioViewModel>>("Usuarios") ?? new();
        }
        public async Task<List<UsuarioViewModel>> GetMozosAsync()
        {
            return await _http.GetFromJsonAsync<List<UsuarioViewModel>>("Usuarios/mozos") ?? new();
        }
    }
}
