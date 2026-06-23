using SeuPet.Domain.Contracts;
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
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAdotanteRepository, AdotanteRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IAdocaoRepository, AdocaoRepository>();
        return services;
    }
    
}