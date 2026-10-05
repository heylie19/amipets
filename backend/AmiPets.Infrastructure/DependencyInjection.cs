using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AmiPets.Application.Interfaces;
using AmiPets.Infrastructure.Data;
using AmiPets.Infrastructure.Repositories;

namespace AmiPets.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AmiPetsDBContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }
}