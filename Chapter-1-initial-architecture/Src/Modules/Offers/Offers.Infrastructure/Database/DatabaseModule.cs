namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Infrastructure.Database;

using Application.Prepare;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repositories;

internal static class DatabaseModule
{
    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OffersPersistenceOptions>(options =>
            configuration.GetSection(OffersPersistenceOptions.SectionName).Bind(options));
        services.AddOptionsWithValidateOnStart<OffersPersistenceOptions>();

        services.AddDbContext<OffersPersistence>((serviceProvider, options) =>
        {
            var opts = serviceProvider.GetRequiredService<IOptions<OffersPersistenceOptions>>();
            options.UseNpgsql(opts.Value.Offers);
        });

        services.AddScoped<IOfferRepository, OfferRepository>();

        return services;
    }

    internal static IApplicationBuilder UseDatabase(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseAutomaticMigrations();
        return applicationBuilder;
    }
}
