namespace SystemGestionReservation.Application.DTOs.Auth;

public class AuthResultDto
{
    public string Token { get; set; }
    public string Login { get; set; }
    public string Role { get; set; }
    public DateTime Expiry { get; set; }
}