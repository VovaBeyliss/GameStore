using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Repositories.Interfaces;

public interface IUserRepository {
    Task<int?> RegisterAsync(UserDto request);
    Task<int?> AuthorizationAsync(UserDto request);
    Task<User?> GetUserByIdAsync(int id);
}