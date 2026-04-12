using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Repositories.Interfaces;

public interface IProductRepository {
    Task AddProductAsync(ProductDto request, int id);
    Task<List<Product>> GetProductsByIdAsync(int id);
    Task DeleteProductsByIdAsync(int id);
}