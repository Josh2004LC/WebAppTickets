using MVCProject.Models;

namespace MVCProject.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Tiquetes>> GetTiquetesAsync();
        Task<Tiquetes> GetTiqueteByIdAsync(int id);
        Task<Tiquetes> CreateTiqueteAsync(Tiquetes tiquete); 
    }
}
