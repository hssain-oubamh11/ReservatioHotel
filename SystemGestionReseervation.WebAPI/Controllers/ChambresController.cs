using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemGestionReservation.Application.DTOs.Chambre;
using SystemGestionReservation.Application.Interfaces;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Web.Controllers;

[Authorize(Roles = "Administrateur,Receptionniste")]
[ApiController]
[Route("api/[controller]")]
public class ChambresController : ControllerBase
{
    private readonly IChambreService _chambreService;

    public ChambresController(IChambreService chambreService)
    {
        _chambreService = chambreService;
    }

    // GET: api/chambres
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var chambres = await _chambreService.GetAllActiveAsync();
        return Ok(chambres);
    }

    // GET: api/chambres/disponibles
    [HttpGet("disponibles")]
    public async Task<IActionResult> Disponibles(
        DateTime dateArrivee,
        DateTime dateDepart,
        TypeChambre? type)
    {
        var chambres = await _chambreService
            .GetDisponiblesAsync(dateArrivee, dateDepart, type);

        return Ok(chambres);
    }

    // GET: api/chambres/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var chambre = await _chambreService.GetByIdAsync(id);

        if (chambre == null)
            return NotFound();

        return Ok(chambre);
    }

    // POST: api/chambres
    [HttpPost]
    public async Task<IActionResult> Create(CreateChambreDto dto)
    {
        await _chambreService.CreateAsync(dto);

        return Ok(new
        {
            message = "Chambre créée avec succès"
        });
    }

    // PUT: api/chambres/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, UpdateChambreDto dto)
    {
        await _chambreService.UpdateAsync(id, dto);

        return Ok(new
        {
            message = "Chambre modifiée avec succès"
        });
    }

    // POST: api/chambres/ajouter-equipement
    [HttpPost("ajouter-equipement")]
    public async Task<IActionResult> AjouterEquipement(
        int chambreId,
        string nomEquipement)
    {
        await _chambreService
            .AjouterEquipementAsync(chambreId, nomEquipement);

        return Ok(new
        {
            message = "Équipement ajouté"
        });
    }

    // DELETE: api/chambres/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Desactiver(int id)
    {
        await _chambreService.DesactiverAsync(id);

        return Ok(new
        {
            message = "Chambre désactivée"
        });
    }
}