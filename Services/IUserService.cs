using TicketsMVC.Models;

namespace TicketsMVC.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersByRoleAsync(int roleId);
        Task<IEnumerable<User>> GetUsersByStatusAsync(string status);
        Task<User> ValidateUserAsync(string email, string password);
        Task<User> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ChangeUserStatusAsync(int id, string newStatus);
        Task<bool> ChangeUserRoleAsync(int id, int newRoleId);
        Task<bool> ChangeUserPasswordAsync(int id, string newPassword);
    }
}
