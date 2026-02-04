using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        AuthResponseDto result = await _authService.LoginAsync(dto);

        if (result == null)
        {
            return Unauthorized("Credenciales incorrectas");
        }
        return Ok(result);
    }
}
