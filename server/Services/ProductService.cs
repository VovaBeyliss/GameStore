using GameStore.Repositories.Interfaces;
using GameStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GameStore.Extensions;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Services;

public class ProductService : IProductService {
    private readonly IProductRepository _productRepository; 

    public ProductService(IProductRepository productRepository) => _productRepository = productRepository;

    public async Task AddOrUpdateProductAsync(AddProductDto dto, int id) {  
        var product = await _productRepository.GetProductByUserIdAndDetailsAsync(p => p.UserId == id && 
                                                                                 p.Name == dto.Name && 
                                                                                 p.Description == dto.Description && 
                                                                                 p.Price == dto.Price);

        if (product == null) {
            var newProduct = new Product {
                Quantity = 1,
                UserId = id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            await _productRepository.AddProductAsync(newProduct);
        } else {
            product.Quantity++;
            await _productRepository.UpdateProductAsync(product);
        }
    }

    public async Task<List<GetProductDto>> GetUserProductsAsync(int id) => (await _productRepository.GetProductsByUserIdAsync(id)).Select(p => p.ToGetProductDto())
                                                                                                                                  .OrderByDescending(p => p.Quantity)
                                                                                                                                  .ToList();

    public async Task DeleteUserProductsAsync(int id) => await _productRepository.DeleteProductsByUserIdAsync(id);
}