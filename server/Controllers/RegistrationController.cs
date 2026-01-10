using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameStore.Services.Interfaces;
using GameStore.Services;
using GameStore.Models;
using GameStore.Dtos;

namespace GameStore.Controllers;

[ApiController]
[Route("api/register")]
public class RegistrationController : ControllerBase {
    private readonly IUserService _userService;

    public RegistrationController(IUserService userService) {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] UserDto request) {
        try {
            var userId = await _userService.RegisterAsync(request);

            if (userId != null) {
                return Ok(new { success = true, userId = userId });
            }

            return Conflict();
        } catch (Exception ex) {
            Console.WriteLine($"Error: {ex.Message}");
            return StatusCode(500, "Error in server!");
        }
    }
}