using Microsoft.EntityFrameworkCore;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Enums;
using SystemGestionReservation.Core.Interfaces;
using SystemGestionReservation.Infrastructure.Data;

namespace SystemGestionReservation.Infrastructure.Repositories;

public class ChambreRepository : EfRepository<Chambre>, IChambreRepository
{
    public ChambreRepository(SystemGestionReservationContext context) : base(context) { }

    public async Task<IEnumerable<Chambre>> GetAllActiveAsync()
        => await _context.Chambres
                         .Include(c => c.Equipements)
                         .Where(c => c.EstActive)
                         .OrderBy(c => c.Numero)
                         .ToListAsync();

    public async Task<IEnumerable<Chambre>> GetDisponiblesAsync(
        DateTime dateArrivee, DateTime dateDepart, TypeChambre? type = null)
    {
        var query = _context.Chambres
            .Include(c => c.Equipements)
            .Where(c => c.EstActive &&
                        !c.Reservations.Any(r =>
                            r.Statut != StatutReservation.Annulee &&
                            r.DateArrivee < dateDepart &&
                            r.DateDepart > dateArrivee));

        if (type.HasValue)
            query = query.Where(c => c.Type == type.Value);

        return await query.OrderBy(c => c.Numero).ToListAsync();
    }

    public new async Task<Chambre?> GetByIdAsync(int id)
        => await _context.Chambres
                         .Include(c => c.Equipements)
                         .Include(c => c.Reservations)
                         .FirstOrDefaultAsync(c => c.Id == id);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}