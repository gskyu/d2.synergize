using _02.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace _02.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IReferencesService, ReferencesService>();

        return services;
    }
}