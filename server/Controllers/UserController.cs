using GameStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameStore.Dtos;

namespace GameStore.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase {
    private readonly IUserService _userService;

    public UserController(IUserService userService) => _userService = userService;

    [HttpPost("regis")]
    public async Task<IActionResult> Register([FromBody] UserDto request) {
        try {
            var id = await _userService.RegisterAsync(request);

            if (id != null) {
                return Ok(new { success = true, userId = id });
            }

            return Conflict();
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500);
        }
    }

    [HttpPost("autho")]
    public async Task<IActionResult> Authorize([FromBody] UserDto request) {
        try {
            var userId = await _userService.AuthorizeAsync(request);

            if (userId != null) {
                return Ok(new { success = true, userId = userId });
            }

            return Unauthorized();
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] int id) {
        try {
            var user = await _userService.GetUserAsync(id);

            if (user != null) {
                return Ok(new { success = true, username = user.Username, email = user.Email });
            }

            return BadRequest();
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500);
        }
    }
}