using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Data;
using GameStore.Dtos;

namespace GameStore.Services;

public class ProductService : IProductService {
    private readonly AppDbContext _db; 

    public ProductService(AppDbContext db) {
        _db = db;
    }

    public async Task AddProduct(ProductDto request, int id) {
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

    public async Task<List<Product>> GetProductsById(int id) {
        var productsList = await _db.Products.ToListAsync();
        var resultProducts = new List<Product>();

        for (int i = 0; i < productsList.Count; i++) {
            if (productsList[i].ProductIdForUser == id) {
                resultProducts.Add(productsList[i]);
            }
        }

        return resultProducts;
    }
}