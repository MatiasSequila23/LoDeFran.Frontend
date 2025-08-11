using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class ComboService
    {
        private readonly HttpClient _http;

        public ComboService(HttpClient http)
        {
            _http = http;
        }

        private const string BaseUrl = "combos";

        public async Task<List<ComboViewModel>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ComboViewModel>>($"{BaseUrl}") ?? new();
        }

        public async Task<ComboViewModel?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ComboViewModel>($"{BaseUrl}/{id}");
        }

        public async Task<bool> CreateAsync(ComboViewModel model)
        {
            var response = await _http.PostAsJsonAsync(BaseUrl, model);
            return response.IsSuccessStatusCode;
        }

        public async Task<int?> CreateAndReturnIdAsync(ComboViewModel model)
        {
            var response = await _http.PostAsJsonAsync($"{BaseUrl}/crear-retornar-id", model);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();
            return result != null && result.ContainsKey("id") ? result["id"] : null;
        }

        public async Task<bool> UpdateAsync(ComboViewModel model)
        {
            var response = await _http.PutAsJsonAsync($"{BaseUrl}/{model.Id}", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CambiarEstadoAsync(int id, int nuevoEstadoId)
        {
            var response = await _http.PatchAsync($"{BaseUrl}/{id}/estado/{nuevoEstadoId}", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> ModificarComentarioComboAsync(int idDetalle, string comentario)
        {
            var content = JsonContent.Create(comentario);
            var response = await _http.PutAsync($"{BaseUrl}/comentario/{idDetalle}", content);
            return response.IsSuccessStatusCode;
        }
    }
}
