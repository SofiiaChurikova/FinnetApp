namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Infrastructure.Database;

internal enum SagaStatus
{
    Started,
    Completed,
    Failed
}

internal sealed class OfferSaga
{
    public Guid SagaId { get; private set; }
    public Guid CorrelationId { get; private set; }
    public SagaStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private OfferSaga() { }

    public static OfferSaga Start(Guid correlationId) =>
        new()
        {
            SagaId = Guid.NewGuid(),
            CorrelationId = correlationId,
            Status = SagaStatus.Started,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

    public void Complete()
    {
        Status = SagaStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }
}
