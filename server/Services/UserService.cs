using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GameStore.Repositories.Interfaces;
using GameStore.Services.Interfaces;
using GameStore.Models;
using GameStore.Data;
using GameStore.Dtos;

namespace GameStore.Services;

public class UserService : IUserService {
    private readonly IUserRepository _userRepository; 

    public UserService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<int?> RegisterAsync(UserDto request) =>  await _userRepository.RegisterAsync(request);

    public async Task<int?> AuthorizationAsync(UserDto request) => await _userRepository.AuthorizationAsync(request);

    public async Task<User?> GetUserByIdAsync(int id) => await _userRepository.GetUserByIdAsync(id);
}