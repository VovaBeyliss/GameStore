using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Services.Interfaces;

public interface IUserService {
    Task<int?> RegisterAsync (UserDto request);
    Task<int?> AuthorizationAsync (UserDto request);
    Task<User?> GetUserById (int id);
}