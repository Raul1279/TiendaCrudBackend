using Entities.Dtos;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IClienteRepository _clienteRepo;
    private readonly IConfiguration _configuration;

    public AuthService(IClienteRepository clienteRepo, IConfiguration config)
    {
        _clienteRepo = clienteRepo;
        _configuration = config;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        Cliente cliente = await _clienteRepo.GetByEmailByCliente(dto.Email);
        if (cliente == null) {
            return null;
        } 

        if (cliente.passwordHash != dto.Password)
        {
            return null;
        }

        string token = GenerateJwt(cliente);

        return new AuthResponseDto
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddHours(10),
            clienteId = cliente.clienteId
        };
    }

    private string GenerateJwt(Cliente cliente)
    {
        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, cliente.clienteId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, cliente.email),
            new Claim("nombre", cliente.nombre)
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
