using MVCProject.Models;
using System.Net.Http.Json;

namespace MVCProject.Services
{
    public class TicketService : ITicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Tiquetes>> GetTiquetesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Tiquetes>>("api/Tiquetes");
        }

        public async Task<Tiquetes> GetTiqueteByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Tiquetes>($"api/Tiquetes/{id}");
        }

        public async Task<Tiquetes> CreateTiqueteAsync(Tiquetes tiquete)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Tiquetes", tiquete);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Tiquetes>();
            }
            else
            {
                // Manejo básico del error 
                throw new Exception("Error al crear el tiquete");
            }
        }
    }
}
