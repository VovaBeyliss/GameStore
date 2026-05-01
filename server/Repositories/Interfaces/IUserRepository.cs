using System.Linq.Expressions;
using System.Threading.Tasks;
using GameStore.Models;

namespace GameStore.Repositories.Interfaces;

public interface IUserRepository {
    Task AddUserAsync(User user);
    Task<User?> GetUserByDetails(Expression<Func<User, bool>> predicate);
    Task<User?> GetUserByIdAsync(int userId);
}