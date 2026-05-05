using SystemGestionReservation.Application.DTOs.Client;
using SystemGestionReservation.Application.Interfaces;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Interfaces;

namespace SystemGestionReservation.Application.Implementations;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        var clients = await _clientRepository.GetAllAsync();
        return clients.Select(ToDto);
    }

    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        return client is null ? null : ToDto(client);
    }

    public async Task<IEnumerable<ClientDto>> SearchAsync(string terme)
    {
        var clients = await _clientRepository.SearchAsync(terme);
        return clients.Select(ToDto);
    }

    public async Task<ClientDto> CreateAsync(CreateClientDto dto)
    {
        var existing = await _clientRepository.GetByNumeroIdentiteAsync(dto.NumeroIdentite);
        if (existing is not null)
            throw new InvalidOperationException($"Un client avec le numéro d'identité '{dto.NumeroIdentite}' existe déjà.");

        var client = new Client(dto.Nom, dto.Prenom, dto.NumeroIdentite,
                                dto.Adresse, dto.Telephone, dto.Email);
        await _clientRepository.AddAsync(client);
        await _clientRepository.SaveChangesAsync();
        return ToDto(client);
    }

    public async Task UpdateAsync(int id, UpdateClientDto dto)
    {
        var client = await _clientRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Client {id} introuvable.");

        client.Modifier(dto.Nom, dto.Prenom, dto.Adresse, dto.Telephone, dto.Email);
        await _clientRepository.UpdateAsync(client);
        await _clientRepository.SaveChangesAsync();
    }

    public async Task DesactiverAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Client {id} introuvable.");

        client.Desactiver();
        await _clientRepository.UpdateAsync(client);
        await _clientRepository.SaveChangesAsync();
    }

    // Mapping manuel
    private static ClientDto ToDto(Client c) => new()
    {
        Id = c.Id,
        Nom = c.Nom,
        Prenom = c.Prenom,
        NumeroIdentite = c.NumeroIdentite,
        Adresse = c.Adresse,
        Telephone = c.Telephone,
        Email = c.Email,
        EstActif = c.EstActif
    };
}