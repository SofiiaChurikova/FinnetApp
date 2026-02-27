namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Domain.Entities;

internal sealed class OffersPersistence(DbContextOptions<OffersPersistence> options) : DbContext(options)
{
    private const string Schema = "Offers";

    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<OfferSaga> OfferSagas => Set<OfferSaga>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfiguration(new OfferEntityConfiguration());

        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Type).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Payload).IsRequired();
            entity.HasIndex(x => new { x.Type, x.CorrelationId }).IsUnique();
        });

        modelBuilder.Entity<OfferSaga>(entity =>
        {
            entity.HasKey(x => x.SagaId);
            entity.Property(x => x.Status).HasConversion<string>();
            entity.HasIndex(x => x.CorrelationId).IsUnique();
        });
    }
}
