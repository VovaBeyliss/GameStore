using GameStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GameStore.Dtos;

namespace GameStore.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase {
    private readonly IUserService _userService;

    public AccountController(IUserService userService) => _userService = userService;

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
