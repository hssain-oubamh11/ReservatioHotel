using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SystemGestionReservation.Application.DTOs.Auth;
using SystemGestionReservation.Application.Interfaces;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Interfaces;

namespace SystemGestionReservation.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUtilisateurRepository _utilisateurRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUtilisateurRepository utilisateurRepository,
                       IConfiguration configuration)
    {
        _utilisateurRepository = utilisateurRepository;
        _configuration = configuration;
    }

    // ── Login ─────────────────────────────────────────────────────
    public async Task<AuthResultDto?> LoginAsync(LoginDto dto)
    {
        var utilisateur = await _utilisateurRepository.GetByLoginAsync(dto.Login);

        if (utilisateur is null || !utilisateur.EstActif)
            return null;

        // Vérifier le mot de passe avec BCrypt
        bool passwordValide = BCrypt.Net.BCrypt.Verify(dto.Password,
                                                        utilisateur.PasswordHash);
        if (!passwordValide)
            return null;

        // Générer le token JWT
        var token = GenererToken(utilisateur);
        var expiry = DateTime.UtcNow.AddHours(8);

        return new AuthResultDto
        {
            Token = token,
            Login = utilisateur.Login,
            Role = utilisateur.Role.ToString(),
            Expiry = expiry
        };
    }

    // ── Register ──────────────────────────────────────────────────
    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        var existing = await _utilisateurRepository.GetByLoginAsync(dto.Login);
        if (existing is not null)
            return false; // Login déjà pris

        // Hachage sécurisé du mot de passe (BCrypt)
        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var utilisateur = new Utilisateur(dto.Login, hash, dto.Role);

        if (dto.ClientId.HasValue)
            utilisateur.LierClient(dto.ClientId.Value);

        await _utilisateurRepository.AddAsync(utilisateur);
        return true;
    }

    // ── Génération du token JWT ───────────────────────────────────
    private string GenererToken(Utilisateur utilisateur)
    {
        var jwtKey = _configuration["Jwt:Key"]!;
        var jwtIssuer = _configuration["Jwt:Issuer"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, utilisateur.Id.ToString()),
            new Claim(ClaimTypes.Name,           utilisateur.Login),
            new Claim(ClaimTypes.Role,           utilisateur.Role.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtIssuer,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}