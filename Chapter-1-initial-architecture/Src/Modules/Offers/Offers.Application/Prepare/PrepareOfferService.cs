namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Application.Prepare;

using System.Text.Json;
using Domain.Entities;

internal sealed class PrepareOfferService(IOfferRepository repository) : IOfferService
{
    public async Task<Offer> PrepareForCustomerAsync(Guid customerId, CancellationToken cancellationToken)
    {
        var offer = Offer.PrepareStandardPassExtension(customerId, DateTimeOffset.UtcNow);

        var outboxPayload = JsonSerializer.Serialize(new
        {
            @event = "OfferPrepared",
            id = offer.Id
        });

        await repository.AddAsync(offer, outboxPayload, cancellationToken);

        return offer;
    }
}
