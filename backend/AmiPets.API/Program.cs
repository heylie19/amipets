
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using AmiPets.Infrastructure.Data;
using AmiPets.Application;
using AmiPets.Application.Interfaces;
using AmiPets.Infrastructure.Repositories;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Cargar las variables de entorno del archivo .env
Env.Load(Path.Combine(builder.Environment.ContentRootPath, ".env"));

// Obtener y validar la cadena de conexión a PostgreSQL
var connectionString = Environment.GetEnvironmentVariable(
    "ConnectionStrings__PostgresConnection"
);

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "No se encontró la cadena de conexión PostgresConnection en el archivo .env."
    );
}

// Configurar la conexión a PostgreSQL
builder.Services.AddDbContext<AmiPetsDBContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar los servicios y validadores de Application
builder.Services.AddApplication();

// Registrar el repositorio de usuarios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Configurar los controladores y la conversión de enums a texto
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Configurar los enums como texto en las respuestas HTTP
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Habilitar la documentación OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Habilitar OpenAPI y Scalar en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
