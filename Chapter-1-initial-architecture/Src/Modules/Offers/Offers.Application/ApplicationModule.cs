namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Application;

using Microsoft.Extensions.DependencyInjection;
using Prepare;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IOfferService, PrepareOfferService>();
        return services;
    }
}
