using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Services.Interfaces;
using GameStore.Services;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase {
    private readonly IProductService _productService;

    public ProductController(IProductService productService) => _productService = productService;

    [HttpPost("{id}")]
    public async Task<IActionResult> AddOrUpdateProduct([FromBody] ProductDto request, [FromRoute] int id) {
        try {
            await _productService.AddOrUpdateProductAsync(request, id);
                    
            return Ok(new { success = true });
        } catch (Exception ex) {
            Console.WriteLine($"Помилка: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserProducts([FromRoute] int id) {
        try {
            return Ok(new { success = true, products = await _productService.GetUserProductsAsync(id) });
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserProducts([FromRoute] int id) {
        try {
            await _productService.DeleteUserProductsAsync(id);

            return Ok(new { success = true });
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }
}