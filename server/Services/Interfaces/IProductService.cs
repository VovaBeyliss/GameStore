using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Services.Interfaces;

public interface IProductService {
    Task AddProductAsync(ProductDto request, int id);
    Task<List<Product>> GetProductsByIdAsync(int id);
    Task DeleteProductsByIdAsync(int id);
}