using GameStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GameStore.Services.Interfaces;
using GameStore.Models;
using GameStore.Data;
using GameStore.Dtos;

namespace GameStore.Repositories;

public class ProductRepository : IProductRepository {
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db) {
        _db = db;
    }

    public async Task AddProductAsync(ProductDto request, int id) {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductIdForUser == id && 
            p.Name == request.Name && 
            p.Description == request.Description && 
            p.Price == request.Price);

        if (product != null) {
            product.Count++;
        } else {
            var newProduct = new Product {
                Count = 1,
                ProductIdForUser = id, 
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            _db.Products.Add(newProduct);
        }

        await _db.SaveChangesAsync();
    }

    public async Task<List<Product>> GetProductsByIdAsync(int id) => await _db.Products.Where(p => p.ProductIdForUser == id).ToListAsync();

    public async Task DeleteProductsByIdAsync(int id) => await _db.Products.Where(p => p.ProductIdForUser == id).ExecuteDeleteAsync();
}