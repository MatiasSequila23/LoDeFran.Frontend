using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class CalleService
    {
        private readonly HttpClient _http;

        public CalleService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CalleViewModel>> BuscarDesdeBase(string nombre) =>
            await _http.GetFromJsonAsync<List<CalleViewModel>>($"calles/buscar?nombre={nombre}") ?? new();

        public async Task<List<CalleViewModel>> BuscarDesdeExcel(string nombre) =>
            await _http.GetFromJsonAsync<List<CalleViewModel>>($"calles/buscar-en-excel?nombre={nombre}") ?? new();

        public async Task ImportarDesdeExcel() =>
            await _http.PostAsync("calles/importar", null);

        public async Task GuardarDesdeExcel(CalleViewModel calle) =>
            await _http.PostAsJsonAsync("calles", calle);

        public async Task EliminarCalle(int id) =>
            await _http.DeleteAsync($"calles/{id}");
    }
}
