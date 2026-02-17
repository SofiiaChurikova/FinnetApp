namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Infrastructure.Repositories;

using Application.Prepare;
using Database;
using Domain.Entities;

internal sealed class OfferRepository(OffersPersistence persistence) : IOfferRepository
{
    public async Task AddAsync(Offer offer, CancellationToken cancellationToken = default)
    {
        persistence.Offers.Add(offer);
        await persistence.SaveChangesAsync(cancellationToken);
    }
}
