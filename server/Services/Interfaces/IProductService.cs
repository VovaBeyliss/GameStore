using System.Threading.Tasks;
using GameStore.Dtos;

namespace GameStore.Services.Interfaces;

public interface IProductService {
    Task AddOrUpdateProductAsync(AddProductDto dto, int id);
    Task<List<GetProductDto>> GetUserProductsAsync(int id);
    Task DeleteUserProductsAsync(int id);
}