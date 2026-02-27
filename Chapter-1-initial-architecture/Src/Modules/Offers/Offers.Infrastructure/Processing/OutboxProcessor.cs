namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Infrastructure.Processing;

using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal sealed class OutboxProcessor(
    IServiceScopeFactory scopeFactory,
    ILogger<OutboxProcessor> logger) : BackgroundService
{
    private static readonly Action<ILogger, string, Guid, Exception?> LogProcessing =
        LoggerMessage.Define<string, Guid>(
            LogLevel.Information,
            new EventId(1, "ProcessingMessage"),
            "Processing outbox message: Type={Type}, CorrelationId={CorrelationId}");

    private static readonly Action<ILogger, Guid, Exception?> LogAlreadyCompleted =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            new EventId(2, "AlreadyCompleted"),
            "Saga {SagaId} already completed, skipping.");

    private static readonly Action<ILogger, Guid, Exception?> LogCompleted =
        LoggerMessage.Define<Guid>(
            LogLevel.Information,
            new EventId(3, "SagaCompleted"),
            "Saga {SagaId} completed.");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessOutboxAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private async Task ProcessOutboxAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<OffersPersistence>();

        var unprocessed = await db.OutboxMessages
            .Where(m => m.ProcessedAt == null)
            .ToListAsync(cancellationToken);

        foreach (var message in unprocessed)
        {
            LogProcessing(logger, message.Type, message.CorrelationId, null);

            var saga = await db.OfferSagas
                .FirstOrDefaultAsync(s => s.CorrelationId == message.CorrelationId, cancellationToken);

            if (saga is not null)
            {
                if (saga.Status == SagaStatus.Completed)
                {
                    LogAlreadyCompleted(logger, saga.SagaId, null);
                    message.MarkAsProcessed();
                    await db.SaveChangesAsync(cancellationToken);
                    continue;
                }

                saga.Complete();
                LogCompleted(logger, saga.SagaId, null);
            }

            message.MarkAsProcessed();
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
