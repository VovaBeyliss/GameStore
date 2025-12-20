using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Models;
using GameStore.Data;

namespace GameStore.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase {
    private readonly AppDbContext _db;

    public AccountController(AppDbContext db) {
        _db = db;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] int id) {
        try {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null) {
                return Ok(new { success = true, username = user.Username, email = user.Email });
            }

            return BadRequest();
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }
}
