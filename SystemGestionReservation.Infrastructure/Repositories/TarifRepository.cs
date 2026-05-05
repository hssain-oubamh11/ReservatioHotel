using Microsoft.EntityFrameworkCore;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Enums;
using SystemGestionReservation.Core.Interfaces;
using SystemGestionReservation.Infrastructure.Data;

namespace SystemGestionReservation.Infrastructure.Repositories;

public class TarifRepository : EfRepository<Tarif>, ITarifRepository
{
    public TarifRepository(SystemGestionReservationContext context) : base(context) { }

    public async Task<IEnumerable<Tarif>> GetAllAsync()
        => await _context.Tarifs
                         .OrderBy(t => t.TypeChambre)
                         .ThenBy(t => t.Saison)
                         .ToListAsync();

    public async Task<Tarif?> GetByTypeSaisonAsync(TypeChambre type, Saison saison)
        => await _context.Tarifs
                         .FirstOrDefaultAsync(t => t.TypeChambre == type
                                                && t.Saison == saison);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}