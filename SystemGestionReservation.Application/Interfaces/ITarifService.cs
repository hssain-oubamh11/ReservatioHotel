using SystemGestionReservation.Application.DTOs.Tarif;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Application.Interfaces;

public interface ITarifService
{
    Task<IEnumerable<TarifDto>> GetAllAsync();
    Task<TarifDto> CreateAsync(CreateTarifDto dto);
    Task<decimal> GetPrixParNuitAsync(TypeChambre type, DateTime date);
    Task UpdateAsync(int id, UpdateTarifDto dto);
}