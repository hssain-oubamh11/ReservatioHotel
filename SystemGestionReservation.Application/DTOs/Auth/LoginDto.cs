using System.ComponentModel.DataAnnotations;

namespace SystemGestionReservation.Application.DTOs.Auth;

public class LoginDto
{
    [Required(ErrorMessage = "Le login est obligatoire.")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
    public string Password { get; set; }
}