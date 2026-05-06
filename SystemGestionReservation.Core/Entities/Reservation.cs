using SystemGestionReservation.SharedKernel;
using SystemGestionReservation.SharedKernel.Interfaces;
using SystemGestionReservation.Core.Enums;
using SystemGestionReservation.Core.Exceptions;

namespace SystemGestionReservation.Core.Entities;

public class Reservation : BaseEntity, IAggregateRoot
{
    public int ClientId { get; private set; }
    public int ChambreId { get; private set; }
    public DateTime DateArrivee { get; private set; }
    public DateTime DateDepart { get; private set; }
    public int NombrePersonnes { get; private set; }
    public StatutReservation Statut { get; private set; }
    public DateTime DateCreation { get; private set; }
    public DateTime? HeureArriveeEffective { get; private set; }
    public decimal? RemiseAppliquee { get; private set; }

    public virtual Client Client { get; private set; }
    public virtual Chambre Chambre { get; private set; }
    public virtual Facture? Facture { get; private set; }

    protected Reservation() { }

    public Reservation(int clientId, int chambreId, DateTime dateArrivee,
                       DateTime dateDepart, int nombrePersonnes)
    {
        if (dateArrivee >= dateDepart)
            throw new ArgumentException(
                "La date d'arrivée doit être avant la date de départ.");
        if (nombrePersonnes <= 0)
            throw new ArgumentException(
                "Le nombre de personnes doit être positif.");

        ClientId = clientId;
        ChambreId = chambreId;
        DateArrivee = dateArrivee;
        DateDepart = dateDepart;
        NombrePersonnes = nombrePersonnes;
        Statut = StatutReservation.EnAttente;
        DateCreation = DateTime.UtcNow;
    }

    // ── Propriété calculée ────────────────────────────────────────
    public int NombreNuits => (DateDepart - DateArrivee).Days;

    // ── Règles métier (énoncé §3.4) ───────────────────────────────
    public void Confirmer() => Statut = StatutReservation.Confirmee;

    public void EffectuerCheckIn()
    {
        if (Statut != StatutReservation.Confirmee &&
            Statut != StatutReservation.EnAttente)
            throw new InvalidOperationException(
                "Le check-in requiert une réservation confirmée ou en attente.");

        Statut = StatutReservation.CheckInEffectue;
        HeureArriveeEffective = DateTime.UtcNow;
    }

    public void EffectuerCheckOut()
    {
        if (Statut != StatutReservation.CheckInEffectue)
            throw new InvalidOperationException(
                "Le check-out requiert un check-in préalable.");

        Statut = StatutReservation.CheckOutEffectue;
    }

    // Règle métier §3.4 — pénalités d'annulation
    public decimal CalculerPenalite()
    {
        var joursAvantArrivee = (DateArrivee - DateTime.UtcNow).Days;

        return joursAvantArrivee switch
        {
            >= 7 => 0m,          // Annulation gratuite si > 7 jours
            >= 3 => 0.25m,       // 25% de pénalité entre 3 et 7 jours
            >= 1 => 0.50m,       // 50% de pénalité entre 1 et 3 jours
            _ => 1.00m        // 100% de pénalité le jour même
        };
    }

    public (decimal PourcentagePenalite, string Message) Annuler()
    {
        if (Statut == StatutReservation.CheckInEffectue ||
            Statut == StatutReservation.CheckOutEffectue)
            throw new InvalidOperationException(
                "Impossible d'annuler une réservation en cours ou terminée.");

        var penalite = CalculerPenalite();
        Statut = StatutReservation.Annulee;

        var message = penalite switch
        {
            0m => "Annulation gratuite.",
            0.25m => "Pénalité de 25% appliquée.",
            0.50m => "Pénalité de 50% appliquée.",
            _ => "Pénalité de 100% appliquée (annulation le jour même)."
        };

        return (penalite * 100, message);
    }

    public void Modifier(DateTime dateArrivee, DateTime dateDepart,
                         int chambreId, int nombrePersonnes)
    {
        if (Statut != StatutReservation.EnAttente &&
            Statut != StatutReservation.Confirmee)
            throw new InvalidOperationException(
                "Modification impossible après le check-in.");
        if (dateArrivee >= dateDepart)
            throw new ArgumentException(
                "La date d'arrivée doit être avant la date de départ.");

        DateArrivee = dateArrivee;
        DateDepart = dateDepart;
        ChambreId = chambreId;
        NombrePersonnes = nombrePersonnes;
    }

    public void AppliquerRemise(decimal pourcentage)
    {
        if (pourcentage < 0 || pourcentage > 100)
            throw new ArgumentException("La remise doit être entre 0 et 100%.");
        RemiseAppliquee = pourcentage;
    }
}