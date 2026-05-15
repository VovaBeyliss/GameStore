using System.Threading.Tasks;
using GameStore.Dtos;

namespace GameStore.Services.Interfaces;

public interface IUserService {
    Task<int?> RegisterAsync(UserDto dto);
    Task<int?> AuthorizeAsync(UserDto dto);
    Task<UserDto?> GetUserAsync(int id);
}