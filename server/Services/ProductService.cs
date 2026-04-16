using GameStore.Repositories.Interfaces;
using GameStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using GameStore.Extensions;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Services;

public class ProductService : IProductService {
    private readonly IProductRepository _productRepository; 

    public ProductService(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    public async Task AddOrUpdateProductAsync(ProductDto request, int userId) {  
        var product = await _productRepository.GetProductByUserIdAndDetailsAsync(userId, request.Name, request.Description, request.Price);

        if (product == null) {
            var newProduct = new Product {
                ProductCount = 1,
                ProductIdForUser = userId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            await _productRepository.AddProductAsync(newProduct);
        } else {
            product.ProductCount++;
            await _productRepository.UpdateProductAsync(product);
        }
    }

    public async Task<List<Product>> GetUserProductsAsync(int id) => await _productRepository.GetProductsByUserIdAsync(id).OrderByAsync(p => p.Id);

    public async Task DeleteUserProductsAsync(int id) => await _productRepository.DeleteProductsByUserIdAsync(id);
}