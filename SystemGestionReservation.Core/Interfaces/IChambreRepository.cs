
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Enums;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Core.Interfaces;

public interface IChambreRepository : IAsyncRepository<Chambre>
{
    Task<IEnumerable<Chambre>> GetAllActiveAsync();
    Task<IEnumerable<Chambre>> GetDisponiblesAsync(DateTime dateArrivee,
                                                    DateTime dateDepart,
                                                    TypeChambre? type = null);
    Task SaveChangesAsync();
}