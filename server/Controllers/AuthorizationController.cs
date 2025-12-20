using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using GameStore.Data;

namespace GameStore.Controllers;

[ApiController]
[Route("api/authorization")]
public class AuthorizationController : ControllerBase {
    private readonly AppDbContext _db;

    public AuthorizationController(AppDbContext db) {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Authorization([FromBody] AuthorizationDto request) {
        try {
            var user = await _db.Users.FirstOrDefaultAsync(u => 
                u.Username == request.Username &&
                u.Password == request.Password && 
                u.Email == request.Email);

            if (user != null) {
                return Ok(new { success = true, userId = user.Id });
            }

            return Unauthorized();
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }
}

public record AuthorizationDto(string Username, string Email, string Password);
