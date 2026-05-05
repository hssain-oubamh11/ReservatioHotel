using SystemGestionReservation.SharedKernel;
using SystemGestionReservation.SharedKernel.Interfaces;
                     
namespace SystemGestionReservation.Core.Entities;

public class Client : BaseEntity, IAggregateRoot
{
    public string Nom { get; private set; }
    public string Prenom { get; private set; }
    public string NumeroIdentite { get; private set; }
    public string Adresse { get; private set; }
    public string Telephone { get; private set; }
    public string Email { get; private set; }
    public bool EstActif { get; private set; }

    public virtual ICollection<Reservation> Reservations { get; private set; }
        = new List<Reservation>();

    protected Client() { }

    public Client(string nom, string prenom, string numeroIdentite,
                  string adresse, string telephone, string email)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new ArgumentException("Le nom est obligatoire.");
        if (string.IsNullOrWhiteSpace(prenom))
            throw new ArgumentException("Le prénom est obligatoire.");
        if (string.IsNullOrWhiteSpace(numeroIdentite))
            throw new ArgumentException("Le numéro d'identité est obligatoire.");

        Nom = nom;
        Prenom = prenom;
        NumeroIdentite = numeroIdentite;
        Adresse = adresse;
        Telephone = telephone;
        Email = email;
        EstActif = true;
    }

    public void Modifier(string nom, string prenom, string adresse,
                         string telephone, string email)
    {
        Nom = nom;
        Prenom = prenom;
        Adresse = adresse;
        Telephone = telephone;
        Email = email;
    }

    public void Desactiver() => EstActif = false;
}