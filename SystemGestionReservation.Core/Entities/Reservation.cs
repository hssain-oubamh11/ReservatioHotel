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

    public void Annuler()
    {
        if (Statut == StatutReservation.CheckInEffectue ||
            Statut == StatutReservation.CheckOutEffectue)
            throw new InvalidOperationException(
                "Impossible d'annuler une réservation en cours ou terminée.");

        Statut = StatutReservation.Annulee;
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