using AppointmentBookingSystem.DTOs;
using AppointmentBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBookingSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _authService.LoginAsync(request);
        
        if (response == null)
            return Unauthorized(new { message = "Invalid username or password" });

        return Ok(response);
    }
}
