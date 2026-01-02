using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Services.Interfaces;
using GameStore.Services;
using GameStore.Models;

namespace GameStore.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase {
    private readonly IUserService _userService;

    public AccountController(IUserService userService) {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] int id) {
        try {
            var user = await _userService.GetUserById(id);

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
