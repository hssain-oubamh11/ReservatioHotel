using Microsoft.EntityFrameworkCore;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Interfaces;
using SystemGestionReservation.Infrastructure.Data;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Infrastructure.Repositories;

public class ClientRepository : EfRepository<Client>, IClientRepository
{
    public ClientRepository(SystemGestionReservationContext context) : base(context) { }

    public async Task<Client?> GetByNumeroIdentiteAsync(string numeroIdentite)
        => await _context.Clients
                         .FirstOrDefaultAsync(c => c.NumeroIdentite == numeroIdentite);

    public async Task<IEnumerable<Client>> SearchAsync(string terme)
        => await _context.Clients
                         .Where(c => c.EstActif &&
                                    (c.Nom.Contains(terme) ||
                                     c.Prenom.Contains(terme) ||
                                     c.NumeroIdentite.Contains(terme) ||
                                     c.Telephone.Contains(terme)))
                         .ToListAsync();

    public async Task<IEnumerable<Client>> GetAllAsync()
        => await _context.Clients
                         .Where(c => c.EstActif)
                         .OrderBy(c => c.Nom)
                         .ToListAsync();

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}