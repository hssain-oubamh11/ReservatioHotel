using Microsoft.AspNetCore.Mvc;
using SystemGestionReservation.Application.DTOs.Client;
using SystemGestionReservation.Application.Interfaces;

namespace SystemGestionReservation.Web.Controllers;

public class ClientsController : Controller
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    // GET: /Clients
    public async Task<IActionResult> Index(string? terme)
    {
        var clients = string.IsNullOrWhiteSpace(terme)
            ? await _clientService.GetAllAsync()
            : await _clientService.SearchAsync(terme);

        ViewBag.Terme = terme;
        return View(clients);
    }

    // GET: /Clients/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client is null) return NotFound();
        return View(client);
    }

    // GET: /Clients/Create
    public IActionResult Create() => View();

    // POST: /Clients/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateClientDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        try
        {
            await _clientService.CreateAsync(dto);
            TempData["Success"] = "Client créé avec succès.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(dto);
        }
    }

    // GET: /Clients/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var client = await _clientService.GetByIdAsync(id);
        if (client is null) return NotFound();

        var dto = new UpdateClientDto
        {
            Nom = client.Nom,
            Prenom = client.Prenom,
            Adresse = client.Adresse,
            Telephone = client.Telephone,
            Email = client.Email
        };
        ViewBag.ClientId = id;
        return View(dto);
    }

    // POST: /Clients/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateClientDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ClientId = id;
            return View(dto);
        }

        try
        {
            await _clientService.UpdateAsync(id, dto);
            TempData["Success"] = "Client modifié avec succès.";
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            ViewBag.ClientId = id;
            return View(dto);
        }
    }

    // POST: /Clients/Desactiver/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Desactiver(int id)
    {
        await _clientService.DesactiverAsync(id);
        TempData["Success"] = "Client désactivé.";
        return RedirectToAction(nameof(Index));
    }
}