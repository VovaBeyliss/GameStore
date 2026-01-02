using System.Threading.Tasks;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Services.Interfaces;

public interface IProductService {
    Task AddProduct(ProductDto request, int id);
    Task<List<Product>> GetProductsById(int id);
}