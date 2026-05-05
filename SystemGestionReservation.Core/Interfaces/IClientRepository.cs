
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Core.Interfaces;

public interface IClientRepository : IAsyncRepository<Client>
{
    Task<Client?> GetByNumeroIdentiteAsync(string numeroIdentite);
    Task<IEnumerable<Client>> SearchAsync(string terme);
    Task<IEnumerable<Client>> GetAllAsync();
    Task SaveChangesAsync();
}