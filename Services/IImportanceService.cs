using TicketsMVC.Models;

namespace TicketsMVC.Services
{
    public interface IImportanceService
    {
        Task<IEnumerable<Importance>> GetAllImportancesAsync();
        Task<Importance> GetImportanceByIdAsync(int id);
        Task<Importance> CreateImportanceAsync(Importance importance);
        Task<bool> UpdateImportanceAsync(int id, Importance importance);
        Task<bool> DeleteImportanceAsync(int id);
    }
}
