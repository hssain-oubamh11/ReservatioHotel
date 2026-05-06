
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Core.Interfaces;

public interface IUtilisateurRepository : IAsyncRepository<Utilisateur>
{
    Task<Utilisateur?> GetByLoginAsync(string login);
    Task<IEnumerable<Utilisateur>> GetAllAsync();
    Task SaveChangesAsync();
}