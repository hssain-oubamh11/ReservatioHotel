using SystemGestionReservation.SharedKernel;
using SystemGestionReservation.SharedKernel.Interfaces;
using SystemGestionReservation.Core.Enums;


namespace SystemGestionReservation.Core.Entities;

public class Chambre : BaseEntity, IAggregateRoot
{
    public string Numero { get; private set; }
    public TypeChambre Type { get; private set; }
    public int Etage { get; private set; }
    public int CapaciteAccueil { get; private set; }
    public string Description { get; private set; }
    public bool EstActive { get; private set; }

    public virtual ICollection<Equipement> Equipements { get; private set; }
        = new List<Equipement>();
    public virtual ICollection<Reservation> Reservations { get; private set; }
        = new List<Reservation>();

    protected Chambre() { }

    public Chambre(string numero, TypeChambre type, int etage,
                   int capaciteAccueil, string description)
    {
        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("Le numéro de chambre est obligatoire.");
        if (capaciteAccueil <= 0)
            throw new ArgumentException("La capacité doit être positive.");

        Numero = numero;
        Type = type;
        Etage = etage;
        CapaciteAccueil = capaciteAccueil;
        Description = description;
        EstActive = true;
    }

    public void Modifier(TypeChambre type, int etage,
                         int capaciteAccueil, string description)
    {
        Type = type;
        Etage = etage;
        CapaciteAccueil = capaciteAccueil;
        Description = description;
    }

    public void AjouterEquipement(Equipement equipement)
    {
        if (!Equipements.Contains(equipement))
            Equipements.Add(equipement);
    }

    public void Desactiver() => EstActive = false;
}