using SystemGestionReservation.SharedKernel;
using SystemGestionReservation.SharedKernel.Interfaces;
using SystemGestionReservation.SharedKernel;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Core.Entities;

public class Equipement : BaseEntity, IAggregateRoot
{
    public string Nom { get; private set; }

    protected Equipement() { }

    public Equipement(string nom)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new ArgumentException("Le nom de l'équipement est obligatoire.");
        Nom = nom;
    }
}