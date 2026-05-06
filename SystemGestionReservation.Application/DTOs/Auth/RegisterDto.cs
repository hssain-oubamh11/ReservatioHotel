using System.ComponentModel.DataAnnotations;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Application.DTOs.Auth;

public class RegisterDto
{
    [Required(ErrorMessage = "Le login est obligatoire.")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
    [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
    public string Password { get; set; }

    [Required]
    public UtilisateurRole Role { get; set; }

    public int? ClientId { get; set; }
}