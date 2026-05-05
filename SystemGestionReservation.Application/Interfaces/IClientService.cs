using SystemGestionReservation.Application.DTOs.Client;

namespace SystemGestionReservation.Application.Interfaces;

public interface IClientService
{
    Task<IEnumerable<ClientDto>> GetAllAsync();
    Task<ClientDto?> GetByIdAsync(int id);
    Task<IEnumerable<ClientDto>> SearchAsync(string terme);
    Task<ClientDto> CreateAsync(CreateClientDto dto);
    Task UpdateAsync(int id, UpdateClientDto dto);
    Task DesactiverAsync(int id);
}