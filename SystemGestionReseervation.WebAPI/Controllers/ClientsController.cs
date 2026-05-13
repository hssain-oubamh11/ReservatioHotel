using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemGestionReservation.Application.DTOs.Client;
using SystemGestionReservation.Application.Interfaces;

namespace SystemGestionReservation.Web.Controllers;

[Authorize(Roles = "Administrateur,Receptionniste")]
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    // GET: api/clients
    [HttpGet]
    public async Task<IActionResult> GetAll(string? terme)
    {
        var clients = string.IsNullOrWhiteSpace(terme)
            ? await _clientService.GetAllAsync()
            : await _clientService.SearchAsync(terme);

        return Ok(clients);
    }

    // GET: api/clients/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var client = await _clientService.GetByIdAsync(id);

        if (client == null)
            return NotFound();

        return Ok(client);
    }

    // POST: api/clients
    [HttpPost]
    public async Task<IActionResult> Create(CreateClientDto dto)
    {
        try
        {
            await _clientService.CreateAsync(dto);
            return Ok(new { message = "Client créé avec succès" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/clients/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, UpdateClientDto dto)
    {
        try
        {
            await _clientService.UpdateAsync(id, dto);
            return Ok(new { message = "Client modifié avec succès" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // DELETE: api/clients/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Desactiver(int id)
    {
        await _clientService.DesactiverAsync(id);

        return Ok(new { message = "Client désactivé" });
    }
}