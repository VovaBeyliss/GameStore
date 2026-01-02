using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Services.Interfaces;

public interface IUserService {
    Task<int?> RegisterAsync (RegistrationDto request);
    Task<int?> AuthorizationAsync (AuthorizationDto request);
    Task<User?> GetUserById (int id);
}