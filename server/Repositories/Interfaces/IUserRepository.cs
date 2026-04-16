using System.Threading.Tasks;
using GameStore.Models;

namespace GameStore.Repositories.Interfaces;

public interface IUserRepository {
    Task AddUserAsync(User user);
    Task<User?> GetUserByDetails(string username, string password, string email);
    Task<User?> GetUserByIdAsync(int userId);
}