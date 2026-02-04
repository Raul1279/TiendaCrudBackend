using Entities;
using Entities.Dtos;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}
