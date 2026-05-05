
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Core.Interfaces;

public interface IReservationRepository : IAsyncRepository<Reservation>
{
    Task<Reservation?> GetByIdAsync(int id);
    Task<IEnumerable<Reservation>> GetByClientIdAsync(int clientId);
    Task<IEnumerable<Reservation>> GetByChambreIdAsync(int chambreId);
    Task SaveChangesAsync();
}