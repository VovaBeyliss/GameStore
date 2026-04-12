using Microsoft.EntityFrameworkCore;
using GameStore.Repositories.Interfaces;
using GameStore.Services.Interfaces;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Services;

public class ProductService : IProductService {
    private readonly IProductRepository _productRepository; 

    public ProductService(IProductRepository productRepository) {
        _productRepository = productRepository;
    }

    public async Task AddProductAsync(ProductDto request, int id) => await _productRepository.AddProductAsync(request, id);

    public async Task<List<Product>> GetProductsByIdAsync(int id) => await _productRepository.GetProductsByIdAsync(id);

    public async Task DeleteProductsByIdAsync(int id) => await _productRepository.DeleteProductsByIdAsync(id);
}