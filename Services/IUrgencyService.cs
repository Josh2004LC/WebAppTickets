using TicketsMVC.Models;

namespace TicketsMVC.Services
{
    public interface IUrgencyService
    {
        Task<IEnumerable<Urgency>> GetAllUrgenciesAsync();
        Task<Urgency> GetUrgencyByIdAsync(int id);
        Task<Urgency> CreateUrgencyAsync(Urgency urgency);
        Task<bool> UpdateUrgencyAsync(int id, Urgency urgency);
        Task<bool> DeleteUrgencyAsync(int id);
    }
}
