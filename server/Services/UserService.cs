using GameStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GameStore.Services.Interfaces;
using System.Threading.Tasks;
using GameStore.Extensions;
using GameStore.Models;
using GameStore.Data;
using GameStore.Dtos;

namespace GameStore.Services;

public class UserService : IUserService {
    private readonly IUserRepository _userRepository; 

    public UserService(IUserRepository userRepository) => _userRepository = userRepository;

    public async Task<int?> RegisterAsync(UserDto dto) {
        var existingUser = await _userRepository.GetUserByDetails(u => u.Username == dto.Username && 
                                                          u.Email == dto.Email && 
                                                          u.Password == dto.Password);

        if (existingUser != null) {
            return null;
        }

        var newUser = new User {
            Username = dto.Username,
            Email = dto.Email,
            Password = dto.Password,
            Photopath = "image.jpg"
        };

        await _userRepository.AddUserAsync(newUser);

        return newUser.Id;
    }

    public async Task<int?> AuthorizationAsync(UserDto dto) {
        var user = await _userRepository.GetUserByDetails(u => u.Username == dto.Username && 
                                                          u.Email == dto.Email && 
                                                          u.Password == dto.Password);

        return user?.Id;
    }

    public async Task<UserDto?> GetUserAsync(int userId) => await _userRepository.GetUserByIdAsync(userId)?.ToUserDtoAsync();
}