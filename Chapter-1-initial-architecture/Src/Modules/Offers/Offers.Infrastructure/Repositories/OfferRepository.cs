namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Infrastructure.Repositories;

using Application.Prepare;
using Database;
using Domain.Entities;

internal sealed class OfferRepository(OffersPersistence persistence) : IOfferRepository
{
    public async Task AddAsync(Offer offer, string outboxPayload, CancellationToken cancellationToken = default)
    {
        persistence.Offers.Add(offer);
        persistence.OutboxMessages.Add(OutboxMessage.Create("OfferPrepared", outboxPayload, offer.Id));
        persistence.OfferSagas.Add(OfferSaga.Start(offer.Id));

        await persistence.SaveChangesAsync(cancellationToken);
    }
}
