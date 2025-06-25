using LoDeFran.Frontend.Models;
using System.Net.Http.Json;

namespace LoDeFran.Frontend.Services
{
    public class ClienteService
    {
        private readonly HttpClient _http;

        public ClienteService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ClienteViewModel>> GetClientesAsync()
        {
            return await _http.GetFromJsonAsync<List<ClienteViewModel>>("Clientes");
        }

        public async Task<ClienteViewModel> GetClienteAsync(int id)
        {
            return await _http.GetFromJsonAsync<ClienteViewModel>($"Clientes/{id}");
        }

        public async Task<ClienteViewModel> CrearClienteAsync(ClienteViewModel cliente)
        {
            var response = await _http.PostAsJsonAsync("Clientes", cliente);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ClienteViewModel>();
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }

        public async Task EditarClienteAsync(int id, ClienteViewModel cliente)
        {
            var response = await _http.PutAsJsonAsync($"Clientes/{id}", cliente);
            response.EnsureSuccessStatusCode();
        }

        public async Task EliminarClienteAsync(int id)
        {
            var response = await _http.DeleteAsync($"Clientes/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

}
