namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Infrastructure.Database;

internal sealed class OutboxMessage
{
    public Guid Id { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Payload { get; private set; } = string.Empty;
    public Guid CorrelationId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }

    private OutboxMessage() { }

    public static OutboxMessage Create(string type, string payload, Guid correlationId) =>
        new()
        {
            Id = Guid.NewGuid(),
            Type = type,
            Payload = payload,
            CorrelationId = correlationId,
            CreatedAt = DateTime.UtcNow
        };

    public void MarkAsProcessed() => ProcessedAt = DateTime.UtcNow;
}
