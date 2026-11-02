
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AmiPets.Application;

// Registro de los servicios de Application
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registrar todos los validadores del proyecto
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
