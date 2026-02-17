namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Infrastructure;

using Application;
using Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class OffersModule
{
    public static IServiceCollection AddOffersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddApplicationServices();

        return services;
    }

    public static IApplicationBuilder UseOffersModule(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseDatabase();
        return applicationBuilder;
    }
}
