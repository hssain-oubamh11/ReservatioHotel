using SystemGestionReservation.Application.DTOs.Auth;

namespace SystemGestionReservation.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResultDto?> LoginAsync(LoginDto dto);
    Task<bool> RegisterAsync(RegisterDto dto);
}