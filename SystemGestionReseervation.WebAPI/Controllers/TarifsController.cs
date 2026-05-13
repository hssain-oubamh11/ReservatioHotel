using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemGestionReservation.Application.DTOs.Tarif;
using SystemGestionReservation.Application.Interfaces;

namespace SystemGestionReservation.WebAPI.Controllers;

[Authorize(Roles = "Administrateur")]
[ApiController]
[Route("api/[controller]")]
public class TarifsController : ControllerBase
{
    private readonly ITarifService _tarifService;

    public TarifsController(ITarifService tarifService)
    {
        _tarifService = tarifService;
    }

    // GET: api/tarifs
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tarifs = await _tarifService.GetAllAsync();
        return Ok(tarifs);
    }

    // PUT: api/tarifs/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTarifDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _tarifService.UpdateAsync(id, dto);
            return Ok(new { message = "Tarif mis à jour avec succès." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}