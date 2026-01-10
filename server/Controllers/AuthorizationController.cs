using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Services.Interfaces;
using GameStore.Services;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Controllers;

[ApiController]
[Route("api/authorization")]
public class AuthorizationController : ControllerBase {
    private readonly IUserService _userService;

    public AuthorizationController(IUserService userService) {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Authorization([FromBody] UserDto request) {
        try {
            var userId = await _userService.AuthorizationAsync(request);

            if (userId != null) {
                return Ok(new { success = true, userId = userId });
            }

            return Unauthorized();
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }
}
