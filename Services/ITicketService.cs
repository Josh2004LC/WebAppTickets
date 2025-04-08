using TicketsMVC.Models;

namespace TicketsMVC.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetTicketsByStatusAsync(string status);
        Task<IEnumerable<Ticket>> GetTicketsByUrgencyAsync(int urgencyId);
        Task<IEnumerable<Ticket>> GetTicketsByUserAsync(int userId);
        Task<IEnumerable<Ticket>> GetTicketsByAnalystAsync(int userId);
        Task<IEnumerable<Ticket>> GetTicketsBySupportAsync(int userId);
        Task<IEnumerable<Ticket>> GetPendingTicketsForAnalystAsync();
        Task<IEnumerable<Ticket>> GetPendingTicketsForSupportAsync();
        Task<IEnumerable<Ticket>> GetResolvedTicketsAsync();
        Task<IEnumerable<Ticket>> GetDailyResolvedTicketsAsync();
        Task<IEnumerable<Ticket>> GetCreatedTicketsAsync();
        Task<IEnumerable<Ticket>> GetDailyCreatedTicketsAsync();
        Task<Ticket> CreateTicketAsync(Ticket ticket);
        Task<bool> UpdateTicketAsync(int id, Ticket ticket);
        Task<bool> DeleteTicketAsync(int id);
        Task<bool> ChangeTicketStatusAsync(int id, string newStatus);
        Task<bool> UpdateTicketSolutionAsync(int id, string solution);
    }
}
