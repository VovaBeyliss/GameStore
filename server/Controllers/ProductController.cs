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

    public ProductController(IProductService productService) {
        _productService = productService;
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto request, [FromRoute] int id) {
        try {
            await _productService.AddProduct(request, id);
                    
            return Ok(new { success = true });
        } catch (Exception ex) {
            Console.WriteLine($"Помилка: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductsById([FromRoute] int id) {
        try {
            var resultProducts = await _productService.GetProductsById(id);

            return Ok(new { success = true, products = resultProducts });
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductsById([FromRoute] int id) {
        await _productService.DeleteProductsById(id);

        return Ok(new { success = true });
    }
}