using SeuPet.Domain.Contracts;
using SeuPet.Domain.Contracts.Repository;
using SeuPet.Domain.Contracts.Services;
using SeuPet.Domain.Services;
using SeuPet.Infra.Repository;
using SeuPet.Infra.Services;

namespace SeuPet.Infra;

public static class DependencyInjector
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAdotanteService, AdotanteService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IAdotanteRepository, AdotanteRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        return services;
    }
}