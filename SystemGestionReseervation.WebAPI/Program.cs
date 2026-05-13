using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SystemGestionReservation.Application.Implementations;
using SystemGestionReservation.Application.Interfaces;
using SystemGestionReservation.Application.Services;
using SystemGestionReservation.Core.Interfaces;
using SystemGestionReservation.Infrastructure.Data;
using SystemGestionReservation.Infrastructure.Repositories;
using SystemGestionReservation.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers + OpenAPI NET10 ────────────────────────────────────
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()));
builder.Services.AddOpenApi();

// ── DbContext — lit depuis appsettings.json ────────────────────────
builder.Services.AddDbContext<SystemGestionReservationContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ── JWT — lit depuis appsettings.json ─────────────────────────────
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"]!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(jwtKey))
        };
    });


builder.Services.AddAuthorization();

// ── Repositories ───────────────────────────────────────────────────
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IChambreRepository, ChambreRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IFactureRepository, FactureRepository>();
builder.Services.AddScoped<ITarifRepository, TarifRepository>();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();

// ── Services ───────────────────────────────────────────────────────
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IChambreService, ChambreService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITarifService, TarifService>();
builder.Services.AddScoped<IFactureService, FactureService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// ── CORS ───────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader()));

var app = builder.Build();

// ── Pipeline ───────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/openapi/v1.json", "SGR Hôtel API v1"));
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();