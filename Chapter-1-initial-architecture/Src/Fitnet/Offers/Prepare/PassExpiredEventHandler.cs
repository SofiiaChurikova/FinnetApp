namespace EvolutionaryArchitecture.Fitnet.Offers.Prepare;

using Modules.Offers.Application;
using Passes.MarkPassAsExpired.Events;
using Common.Events;
using Common.Events.EventBus;

internal sealed class PassExpiredEventHandler(
    IEventBus eventBus,
    IOfferService offerService,
    TimeProvider timeProvider) : IIntegrationEventHandler<PassExpiredEvent>
{
    public async Task Handle(PassExpiredEvent @event, CancellationToken cancellationToken)
    {
        var offer = await offerService.PrepareForCustomerAsync(@event.CustomerId, cancellationToken);

        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId, timeProvider.GetUtcNow());
        await eventBus.PublishAsync(offerPreparedEvent, cancellationToken);
    }
}
