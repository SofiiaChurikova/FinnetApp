namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Application;

using Domain.Entities;

public interface IOfferService
{
    Task<Offer> PrepareForCustomerAsync(Guid customerId, CancellationToken cancellationToken);
}
