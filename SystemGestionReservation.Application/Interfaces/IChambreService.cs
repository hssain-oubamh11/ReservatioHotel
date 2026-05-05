using SystemGestionReservation.Application.DTOs.Chambre;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Application.Interfaces;

public interface IChambreService
{
    Task<IEnumerable<ChambreDto>> GetAllActiveAsync();
    Task<ChambreDto?> GetByIdAsync(int id);
    Task<IEnumerable<ChambreDto>> GetDisponiblesAsync(DateTime dateArrivee,
                                                       DateTime dateDepart,
                                                       TypeChambre? type = null);
    Task<ChambreDto> CreateAsync(CreateChambreDto dto);
    Task UpdateAsync(int id, UpdateChambreDto dto);
    Task AjouterEquipementAsync(int chambreId, string nom);
    Task DesactiverAsync(int id);
}