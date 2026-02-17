namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

internal sealed class OfferEntityConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offers");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.PreparedAt).IsRequired();
        builder.Property(o => o.OfferedFromTo).IsRequired();
        builder.Property(o => o.OfferedFromDate).IsRequired();
        builder.Property(o => o.Discount).IsRequired();
        builder.Property(o => o.CustomerId).IsRequired();
    }
}
