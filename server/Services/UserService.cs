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

    public async Task<int?> RegisterAsync(UserDto request) {
        var existingUser = await _userRepository.GetUserByDetails(request.Username, request.Password, request.Email);

        if (existingUser != null) {
            return null;
        }

        var newUser = new User {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            Photopath = "image.jpg"
        };

        await _userRepository.AddUserAsync(newUser);

        return newUser.Id;
    }

    public async Task<int?> AuthorizationAsync(UserDto request) {
        var user = await _userRepository.GetUserByDetails(request.Username, request.Password, request.Email);

        return user?.Id;
    }

    public async Task<User?> GetUserAsync(int userId) => await _userRepository.GetUserByIdAsync(userId);
}