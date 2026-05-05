using Microsoft.EntityFrameworkCore;
using SystemGestionReservation.Application.Interfaces;
using SystemGestionReservation.Core.Interfaces;
using SystemGestionReservation.Infrastructure.Data;
using SystemGestionReservation.Infrastructure.Repositories;
using Swashbuckle.AspNetCore;
using SystemGestionReservation.Application.Implementations;

var builder = WebApplication.CreateBuilder(args);

// ── Web API (pas MVC) ──────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── Base de données — SQL Server Express ───────────────────────────
builder.Services.AddDbContext<SystemGestionReservationContext>(options =>
    options.UseSqlServer(
        @"Data Source=.\SQLEXPRESS;
          Database=Hotel_MINIDB;
          Integrated Security=True;
          Persist Security Info=False;
          Pooling=False;
          MultipleActiveResultSets=False;
          Encrypt=True;
          TrustServerCertificate=True;
          Command Timeout=0"));

// ── Repositories (Infrastructure) ─────────────────────────────────
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IChambreRepository, ChambreRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IFactureRepository, FactureRepository>();
builder.Services.AddScoped<ITarifRepository, TarifRepository>();

// ── Services (Application) ────────────────────────────────────────
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IChambreService, ChambreService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IFactureService, FactureService>();
builder.Services.AddScoped<ITarifService, TarifService>();

// ── CORS (optionnel — si frontend séparé) ─────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// ── Swagger uniquement en développement ───────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

// ── Route API ─────────────────────────────────────────────────────
app.MapControllers();

app.Run();