
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Core.Interfaces;

public interface IFactureRepository : IAsyncRepository<Facture>
{
    Task<Facture?> GetByReservationIdAsync(int reservationId);
    Task<IEnumerable<Facture>> GetByClientIdAsync(int clientId);
    Task SaveChangesAsync();
}