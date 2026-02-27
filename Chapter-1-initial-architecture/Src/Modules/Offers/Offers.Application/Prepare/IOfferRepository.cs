namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Application.Prepare;

using Domain.Entities;

public interface IOfferRepository
{
    Task AddAsync(Offer offer, string outboxPayload, CancellationToken cancellationToken = default);
}
