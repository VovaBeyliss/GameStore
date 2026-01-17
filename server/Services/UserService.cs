using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using GameStore.Services.Interfaces;
using GameStore.Models;
using GameStore.Data;
using GameStore.Dtos;
using System.Linq;

namespace GameStore.Services;

public class UserService : IUserService {
    private readonly AppDbContext _db; 

    public UserService(AppDbContext db) {
        _db = db;
    }

    public async Task<int?> RegisterAsync(UserDto request) {
        if (await _db.Users.AnyAsync(u => u.Username == request.Username || u.Email == request.Email)) {
            return null;
        }

        var user = new User { 
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            Photopath = "photopath"
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return user.Id;
    }

    public async Task<int?> AuthorizationAsync(UserDto request) {
        var user = await _db.Users.FirstOrDefaultAsync(u => 
            u.Username == request.Username &&
            u.Password == request.Password && 
            u.Email == request.Email);

        return user?.Id;
    }

    public async Task<User?> GetUserById(int id) {
        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}