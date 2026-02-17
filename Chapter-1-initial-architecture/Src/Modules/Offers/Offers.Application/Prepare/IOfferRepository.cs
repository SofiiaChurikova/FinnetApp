namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Application.Prepare;

using Domain.Entities;

public interface IOfferRepository
{
    Task AddAsync(Offer offer, CancellationToken cancellationToken = default);
}
