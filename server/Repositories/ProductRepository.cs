using GameStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using GameStore.Services.Interfaces;
using System.Linq.Expressions;
using GameStore.Models;
using GameStore.Data;

namespace GameStore.Repositories;

public class ProductRepository : IProductRepository {
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<Product?> GetProductByUserIdAndDetailsAsync(Expression<Func<Product, bool>> predicate) => await _db.Products.FirstOrDefaultAsync(predicate);

    public async Task AddProductAsync(Product product) {
        await _db.Products.AddAsync(product);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product) {
        _db.Products.Update(product);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Product>> GetProductsByUserIdAsync(int id) => await _db.Products.Where(p => p.ProductIdForUser == id).ToListAsync();

    public async Task DeleteProductsByUserIdAsync(int id) => await _db.Products.Where(p => p.ProductIdForUser == id).ExecuteDeleteAsync();
}