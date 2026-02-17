namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Application.Prepare;

using Domain.Entities;

internal sealed class PrepareOfferService(IOfferRepository repository) : IOfferService
{
    public async Task<Offer> PrepareForCustomerAsync(Guid customerId, CancellationToken cancellationToken)
    {
        var offer = Offer.PrepareStandardPassExtension(customerId, DateTimeOffset.UtcNow);
        await repository.AddAsync(offer, cancellationToken);
        return offer;
    }
}
