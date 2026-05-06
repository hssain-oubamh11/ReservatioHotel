using Microsoft.AspNetCore.Mvc;
using SystemGestionReservation.Application.DTOs.Auth;
using SystemGestionReservation.Application.Interfaces;

namespace SystemGestionReservation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _authService.LoginAsync(dto);
        if (result is null)
            return Unauthorized(new { message = "Login ou mot de passe incorrect." });

        return Ok(result);
    }

    // POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var success = await _authService.RegisterAsync(dto);
        if (!success)
            return Conflict(new { message = "Ce login est déjà utilisé." });

        return Ok(new { message = "Compte créé avec succès." });
    }
}