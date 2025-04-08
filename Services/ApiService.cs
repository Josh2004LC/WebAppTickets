using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TicketsMVC.Models;

namespace TicketsMVC.Services
{
    public class ApiService : ITicketService, IUserService, ICategoryService, IUrgencyService, IImportanceService, IRoleService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ILogger<ApiService> _logger;

        public ApiService(IHttpClientFactory httpClientFactory, ILogger<ApiService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("TicketsAPI");
            _logger = logger;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        #region Tickets

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Tiquetes");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all tickets");
                throw;
            }
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Tiquetes/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Ticket>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting ticket with ID {id}");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(string status)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Tiquetes/Estado/{status}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting tickets with status {status}");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUrgencyAsync(int urgencyId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Tiquetes/Urgencia/{urgencyId}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting tickets with urgency ID {urgencyId}");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Tiquetes/Usuario/{userId}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting tickets for user ID {userId}");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByAnalystAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Tiquetes/analista/{userId}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting tickets for analyst ID {userId}");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketsBySupportAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Tiquetes/soporte/{userId}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting tickets for support ID {userId}");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetPendingTicketsForAnalystAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Tiquetes/analista/pendientes");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting pending tickets for analyst");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetPendingTicketsForSupportAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Tiquetes/soporte/pendientes");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting pending tickets for support");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetResolvedTicketsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Tiquetes/analista/resueltos");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting resolved tickets");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetDailyResolvedTicketsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Tiquetes/analista/resueltos/diario");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting daily resolved tickets");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetCreatedTicketsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Tiquetes/soporte/creados");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting created tickets");
                throw;
            }
        }

        public async Task<IEnumerable<Ticket>> GetDailyCreatedTicketsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Tiquetes/soporte/creados/diarios");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Ticket>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting daily created tickets");
                throw;
            }
        }

        public async Task<Ticket> CreateTicketAsync(Ticket ticket)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(ticket), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Tiquetes", content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Ticket>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ticket");
                throw;
            }
        }

        public async Task<bool> UpdateTicketAsync(int id, Ticket ticket)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(ticket), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Tiquetes/{id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating ticket with ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Tiquetes/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting ticket with ID {id}");
                throw;
            }
        }

        public async Task<bool> ChangeTicketStatusAsync(int id, string newStatus)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(newStatus), Encoding.UTF8, "application/json");
                var response = await _httpClient.PatchAsync($"api/Tiquetes/{id}/CambiarEstado", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error changing status for ticket with ID {id}");
                throw;
            }
        }

        public async Task<bool> UpdateTicketSolutionAsync(int id, string solution)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(solution), Encoding.UTF8, "application/json");
                var response = await _httpClient.PatchAsync($"api/Tiquetes/analista/actualizarSolucionTicket/{id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating solution for ticket with ID {id}");
                throw;
            }
        }

        #endregion

        #region Users

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Usuarios");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<User>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Usuarios/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<User>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user with ID {id}");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Usuarios/Rol/{roleId}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<User>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting users with role ID {roleId}");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUsersByStatusAsync(string status)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Usuarios/Estado/{status}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<User>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting users with status {status}");
                throw;
            }
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Usuarios/validarIngreso?correo={email}&clave={password}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<User>(_jsonOptions);
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error validating user with email {email}");
                throw;
            }
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Usuarios", content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<User>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }

        public async Task<bool> UpdateUserAsync(int id, User user)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Usuarios/{id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating user with ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Usuarios/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with ID {id}");
                throw;
            }
        }

        public async Task<bool> ChangeUserStatusAsync(int id, string newStatus)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(newStatus), Encoding.UTF8, "application/json");
                var response = await _httpClient.PatchAsync($"api/Usuarios/{id}/CambiarEstado", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error changing status for user with ID {id}");
                throw;
            }
        }

        public async Task<bool> ChangeUserRoleAsync(int id, int newRoleId)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(newRoleId), Encoding.UTF8, "application/json");
                var response = await _httpClient.PatchAsync($"api/Usuarios/{id}/CambiarRol", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error changing role for user with ID {id}");
                throw;
            }
        }

        public async Task<bool> ChangeUserPasswordAsync(int id, string newPassword)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(newPassword), Encoding.UTF8, "application/json");
                var response = await _httpClient.PatchAsync($"api/Usuarios/{id}/CambiarClave", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error changing password for user with ID {id}");
                throw;
            }
        }

        #endregion

        #region Categories

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Categorias");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Category>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all categories");
                throw;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Categorias/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Category>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting category with ID {id}");
                throw;
            }
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Categorias", content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Category>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating category");
                throw;
            }
        }

        public async Task<bool> UpdateCategoryAsync(int id, Category category)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Categorias/{id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating category with ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Categorias/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting category with ID {id}");
                throw;
            }
        }

        #endregion

        #region Urgencies

        public async Task<IEnumerable<Urgency>> GetAllUrgenciesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Urgencias");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Urgency>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all urgencies");
                throw;
            }
        }

        public async Task<Urgency> GetUrgencyByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Urgencias/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Urgency>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting urgency with ID {id}");
                throw;
            }
        }

        public async Task<Urgency> CreateUrgencyAsync(Urgency urgency)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(urgency), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Urgencias", content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Urgency>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating urgency");
                throw;
            }
        }

        public async Task<bool> UpdateUrgencyAsync(int id, Urgency urgency)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(urgency), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Urgencias/{id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating urgency with ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteUrgencyAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Urgencias/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting urgency with ID {id}");
                throw;
            }
        }

        #endregion

        #region Importances

        public async Task<IEnumerable<Importance>> GetAllImportancesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Importancias");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Importance>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all importances");
                throw;
            }
        }

        public async Task<Importance> GetImportanceByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Importancias/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Importance>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting importance with ID {id}");
                throw;
            }
        }

        public async Task<Importance> CreateImportanceAsync(Importance importance)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(importance), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Importancias", content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Importance>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating importance");
                throw;
            }
        }

        public async Task<bool> UpdateImportanceAsync(int id, Importance importance)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(importance), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Importancias/{id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating importance with ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteImportanceAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Importancias/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting importance with ID {id}");
                throw;
            }
        }

        #endregion

        #region Roles

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Roles");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<IEnumerable<Role>>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all roles");
                throw;
            }
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Roles/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Role>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting role with ID {id}");
                throw;
            }
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(role), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Roles", content);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Role>(_jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating role");
                throw;
            }
        }

        public async Task<bool> UpdateRoleAsync(int id, Role role)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(role), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Roles/{id}", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating role with ID {id}");
                throw;
            }
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Roles/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting role with ID {id}");
                throw;
            }
        }

        #endregion
    }
}
