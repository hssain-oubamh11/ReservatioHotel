using SystemGestionReservation.SharedKernel;
using SystemGestionReservation.SharedKernel.Interfaces;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Core.Entities;

public class Tarif : BaseEntity, IAggregateRoot
{
    public TypeChambre TypeChambre { get; private set; }
    public Saison Saison { get; private set; }
    public decimal PrixParNuit { get; private set; }

    protected Tarif() { }

    public Tarif(TypeChambre typeChambre, Saison saison, decimal prixParNuit)
    {
        if (prixParNuit <= 0)
            throw new ArgumentException("Le prix doit être positif.");

        TypeChambre = typeChambre;
        Saison = saison;
        PrixParNuit = prixParNuit;
    }

    public void ModifierPrix(decimal nouveauPrix)
    {
        if (nouveauPrix <= 0)
            throw new ArgumentException("Le prix doit être positif.");
        PrixParNuit = nouveauPrix;
    }
}