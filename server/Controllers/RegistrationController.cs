using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using GameStore.Data;

namespace GameStore.Controllers;

[ApiController]
[Route("api/register")]
public class RegistrationController : ControllerBase {
    private readonly AppDbContext _db;

    public RegistrationController(AppDbContext db) {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] RegistrationDto request) {
        try {
            if (await _db.Users.AnyAsync(u => u.Username == request.Username || u.Email == request.Email)) {
                return Conflict();
            }

            var user = new User { 
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Photopath = "photopath"
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new { success = true, productId = user.Id });
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }
}

public record RegistrationDto(string Username, string Email, string Password);