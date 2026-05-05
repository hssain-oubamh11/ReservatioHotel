
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Enums;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Core.Interfaces;

public interface ITarifRepository : IAsyncRepository<Tarif>
{
    Task<IEnumerable<Tarif>> GetAllAsync();
    Task<Tarif?> GetByTypeSaisonAsync(TypeChambre type, Saison saison);
    Task SaveChangesAsync();
}