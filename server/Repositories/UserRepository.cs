using GameStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Data;

namespace GameStore.Repositories;

public class UserRepository : IUserRepository {
    private readonly AppDbContext _db; 

    public UserRepository(AppDbContext db) => _db = db;

    public async Task AddUserAsync(User user) {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User?> GetUserByDetails(Expression<Func<User, bool>> predicate) => await _db.Users.FirstOrDefaultAsync(predicate);

    public async Task<User?> GetUserByIdAsync(int userId) => await _db.Users.FindAsync(userId);
}