using _03.Infrastructure.Apis;
using _03.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace _03.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IBungieService, BungieService>();
        
        // Clients
        services.AddRefitClient<IBungieApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://www.bungie.net");
                // TODO: Add support for dotnet user secrets and setup local asp net core env
                c.DefaultRequestHeaders.Add("X-API-Key", "TODO");
            });
        
        return services;
    }
}