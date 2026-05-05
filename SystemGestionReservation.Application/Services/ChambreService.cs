using SystemGestionReservation.Application.DTOs.Chambre;
using SystemGestionReservation.Application.Interfaces;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Enums;
using SystemGestionReservation.Core.Interfaces;

namespace SystemGestionReservation.Application.Implementations;

public class ChambreService : IChambreService
{
    private readonly IChambreRepository _chambreRepository;

    public ChambreService(IChambreRepository chambreRepository)
    {
        _chambreRepository = chambreRepository;
    }

    public async Task<IEnumerable<ChambreDto>> GetAllActiveAsync()
    {
        var chambres = await _chambreRepository.GetAllActiveAsync();
        return chambres.Select(ToDto);
    }

    public async Task<ChambreDto?> GetByIdAsync(int id)
    {
        var chambre = await _chambreRepository.GetByIdAsync(id);
        return chambre is null ? null : ToDto(chambre);
    }

    public async Task<IEnumerable<ChambreDto>> GetDisponiblesAsync(
        DateTime dateArrivee, DateTime dateDepart, TypeChambre? type = null)
    {
        var chambres = await _chambreRepository.GetDisponiblesAsync(dateArrivee, dateDepart, type);
        return chambres.Select(ToDto);
    }

    public async Task<ChambreDto> CreateAsync(CreateChambreDto dto)
    {
        var chambre = new Chambre(dto.Numero, dto.Type, dto.Etage,
                                  dto.CapaciteAccueil, dto.Description);
        await _chambreRepository.AddAsync(chambre);
        await _chambreRepository.SaveChangesAsync();
        return ToDto(chambre);
    }

    public async Task UpdateAsync(int id, UpdateChambreDto dto)
    {
        var chambre = await _chambreRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Chambre {id} introuvable.");

        chambre.Modifier(dto.Type, dto.Etage, dto.CapaciteAccueil, dto.Description);
        await _chambreRepository.UpdateAsync(chambre);
        await _chambreRepository.SaveChangesAsync();
    }

    public async Task AjouterEquipementAsync(int chambreId, string nomEquipement)
    {
        var chambre = await _chambreRepository.GetByIdAsync(chambreId)
            ?? throw new KeyNotFoundException($"Chambre {chambreId} introuvable.");

        var equipement = new Equipement(nomEquipement);
        chambre.AjouterEquipement(equipement);
        await _chambreRepository.UpdateAsync(chambre);
        await _chambreRepository.SaveChangesAsync();
    }

    public async Task DesactiverAsync(int id)
    {
        var chambre = await _chambreRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Chambre {id} introuvable.");

        chambre.Desactiver();
        await _chambreRepository.UpdateAsync(chambre);
        await _chambreRepository.SaveChangesAsync();
    }

    //Mapping manuel 
    private static ChambreDto ToDto(Chambre c) => new()
    {
        Id = c.Id,
        Numero = c.Numero,
        Type = c.Type,
        Etage = c.Etage,
        CapaciteAccueil = c.CapaciteAccueil,
        Description = c.Description,
        EstActive = c.EstActive,
        Equipements = c.Equipements.Select(e => e.Nom).ToList()
    };
}