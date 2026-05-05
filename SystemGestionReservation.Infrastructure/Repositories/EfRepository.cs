using Microsoft.EntityFrameworkCore;
using SystemGestionReservation.Infrastructure.Data;
using SystemGestionReservation.SharedKernel;
using SystemGestionReservation.SharedKernel.Interfaces;

namespace SystemGestionReservation.Infrastructure.Repositories;

public class EfRepository<T> : IAsyncRepository<T>
    where T : BaseEntity, IAggregateRoot
{
    protected readonly SystemGestionReservationContext _context;

    public EfRepository(SystemGestionReservationContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id)
        => await _context.Set<T>().FindAsync(id);

    public async Task<IReadOnlyList<T>> ListAllAsync()
        => await _context.Set<T>().ToListAsync();

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(ISpecification<T> spec)
    {
        throw new NotImplementedException();
    }
}