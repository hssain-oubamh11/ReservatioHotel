
using SystemGestionReservation.SharedKernel;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Core.Entities;

public class Facture : BaseEntity, IAggregateRoot
{
    public int ReservationId { get; private set; }
    public DateTime DateEmission { get; private set; }
    public decimal MontantTotal { get; private set; }
    public bool EstPayee { get; private set; }

    public virtual Reservation Reservation { get; private set; }
    public virtual ICollection<LigneFacture> Lignes { get; private set; }
        = new List<LigneFacture>();

    protected Facture() { }

    public Facture(int reservationId,
                   IEnumerable<LigneFacture> lignes,
                   decimal remisePourcentage = 0)
    {
        ReservationId = reservationId;
        DateEmission = DateTime.UtcNow;
        EstPayee = false;

        foreach (var ligne in lignes)
            Lignes.Add(ligne);

        var sousTotal = Lignes.Sum(l => l.SousTotal);
        MontantTotal = sousTotal * (1 - remisePourcentage / 100);
    }

    public void MarquerPayee() => EstPayee = true;
}