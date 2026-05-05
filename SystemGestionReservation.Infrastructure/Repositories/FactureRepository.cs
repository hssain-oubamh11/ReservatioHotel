using Microsoft.EntityFrameworkCore;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Interfaces;
using SystemGestionReservation.Infrastructure.Data;

namespace SystemGestionReservation.Infrastructure.Repositories;

public class FactureRepository : EfRepository<Facture>, IFactureRepository
{
    public FactureRepository(SystemGestionReservationContext context) : base(context) { }

    public new async Task<Facture?> GetByIdAsync(int id)
        => await _context.Factures
                         .Include(f => f.Lignes)
                         .Include(f => f.Reservation)
                             .ThenInclude(r => r.Client)
                         .Include(f => f.Reservation)
                             .ThenInclude(r => r.Chambre)
                         .FirstOrDefaultAsync(f => f.Id == id);

    public async Task<Facture?> GetByReservationIdAsync(int reservationId)
        => await _context.Factures
                         .Include(f => f.Lignes)
                         .Include(f => f.Reservation)
                             .ThenInclude(r => r.Client)
                         .Include(f => f.Reservation)
                             .ThenInclude(r => r.Chambre)
                         .FirstOrDefaultAsync(f => f.ReservationId == reservationId);

    public async Task<IEnumerable<Facture>> GetByClientIdAsync(int clientId)
        => await _context.Factures
                         .Include(f => f.Lignes)
                         .Include(f => f.Reservation)
                             .ThenInclude(r => r.Chambre)
                         .Where(f => f.Reservation.ClientId == clientId)
                         .OrderByDescending(f => f.DateEmission)
                         .ToListAsync();

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}