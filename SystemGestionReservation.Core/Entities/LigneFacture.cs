using SystemGestionReservation.SharedKernel;

namespace SystemGestionReservation.Core.Entities;

public class LigneFacture : BaseEntity
{
    public int FactureId { get; private set; }
    public string Description { get; private set; }
    public int Quantite { get; private set; }
    public decimal PrixUnitaire { get; private set; }

    public decimal SousTotal => Quantite * PrixUnitaire;

    protected LigneFacture() { }

    public LigneFacture(string description, int quantite, decimal prixUnitaire)
    {
        Description = description;
        Quantite = quantite;
        PrixUnitaire = prixUnitaire;
    }
}