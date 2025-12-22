using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using GameStore.Data;

namespace GameStore.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase {
    private readonly AppDbContext _db;

    public ProductController(AppDbContext db) {
        _db = db;
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto request, [FromRoute] int id) {
        try {
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
                    
            return Ok(new { success = true });
        } catch (Exception ex) {
            Console.WriteLine($"Помилка: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductsForId([FromRoute] int id) {
        try {
            var productsList = await _db.Products.ToListAsync();
            var resultProducts = new List<Product>();

            for (int i = 0; i < productsList.Count; i++) {
                if (productsList[i].ProductIdForUser == id) {
                    resultProducts.Add(productsList[i]);
                }
            }

            return Ok(new { success = true, products = resultProducts });
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }
}

public record ProductDto(string Name, string Description, string Price);
