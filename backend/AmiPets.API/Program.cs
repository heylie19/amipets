
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using AmiPets.Infrastructure.Data;
using AmiPets.Application;
using AmiPets.Application.Interfaces;
using AmiPets.Infrastructure.Repositories;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = Environment.GetEnvironmentVariable(
    "ConnectionStrings__PostgresConnection"
) ?? throw new InvalidOperationException(
    "Falta la cadena de conexión a PostgreSQL."
);

var corsAllowedOrigins = Environment.GetEnvironmentVariable(
    "CORS_ALLOWED_ORIGINS"
) ?? "http://localhost:5173";

// Registro de PostgreSQL y las dependencias de AmiPets
builder.Services.AddDbContext<AmiPetsDBContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddApplication();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Configuración CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaCorsAmiPets", policy =>
    {
        var origenes = corsAllowedOrigins.Split(
            ',',
            StringSplitOptions.RemoveEmptyEntries |
            StringSplitOptions.TrimEntries
        );

        policy.WithOrigins(origenes)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Controladores y serialización de enumeraciones
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("PoliticaCorsAmiPets");

app.UseAuthorization();

app.MapControllers();

app.Run();
