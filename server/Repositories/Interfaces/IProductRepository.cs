using System.Linq.Expressions;
using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Repositories.Interfaces;

public interface IProductRepository {
    Task<Product?> GetProductByUserIdAndDetailsAsync(Expression<Func<Product, bool>> predicate);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task<List<Product>> GetProductsByUserIdAsync(int userId);
    Task DeleteProductsByUserIdAsync(int userId);
}