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

    [HttpPost]
    public async Task<IActionResult> ProductToDb([FromBody] ProductDto request, int id) {
        try {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == request.Name && 
                p.Description == request.Description && 
                p.Price == request.Price);

            if (product != null) {
                product.Count++;
            } else {
                var newProduct = new Product {
                    Count = 1,
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

    [HttpGet]
    public async Task<IActionResult> GetProducts() {
        try {
            var productsList = await _db.Products.ToListAsync();

            return Ok(new { success = true, products = productsList });
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }
}

public record ProductDto(string Name, string Description, string Price);
