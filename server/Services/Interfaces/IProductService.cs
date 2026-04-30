using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Services.Interfaces;

public interface IProductService {
    Task AddOrUpdateProductAsync(ProductDto dto, int id);
    Task<List<Product>> GetUserProductsAsync(int id);
    Task DeleteUserProductsAsync(int id);
}