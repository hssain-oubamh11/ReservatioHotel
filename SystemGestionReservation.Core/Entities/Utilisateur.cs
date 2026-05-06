
using SystemGestionReservation.Core.Enums;
using SystemGestionReservation.SharedKernel;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Core.Entities;

public class Utilisateur : BaseEntity, IAggregateRoot
{
    public string Login { get; private set; }
    public string PasswordHash { get; private set; }
    public UtilisateurRole Role { get; private set; }
    public bool EstActif { get; private set; }

    // Lien optionnel vers un client
    public int? ClientId { get; private set; }
    public virtual Client? Client { get; private set; }

    protected Utilisateur() { }

    public Utilisateur(string login, string passwordHash, UtilisateurRole role)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Le login est obligatoire.");
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Le mot de passe est obligatoire.");

        Login = login;
        PasswordHash = passwordHash;
        Role = role;
        EstActif = true;
    }

    public void ChangerMotDePasse(string newHash) => PasswordHash = newHash;
    public void Desactiver() => EstActif = false;
    public void LierClient(int clientId) => ClientId = clientId;
}