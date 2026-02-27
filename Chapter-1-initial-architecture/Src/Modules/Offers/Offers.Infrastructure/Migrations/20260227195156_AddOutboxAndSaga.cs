#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Modules.Offers.Infrastructure.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class AddOutboxAndSaga : Migration
{
    private static readonly string[] ColumnsArray = ["Type", "CorrelationId"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Offers");

        migrationBuilder.CreateTable(
            name: "Offers",
            schema: "Offers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                PreparedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                Discount = table.Column<decimal>(type: "numeric", nullable: false),
                OfferedFromDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                OfferedFromTo = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Offers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "OfferSagas",
            schema: "Offers",
            columns: table => new
            {
                SagaId = table.Column<Guid>(type: "uuid", nullable: false),
                CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                Status = table.Column<string>(type: "text", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OfferSagas", x => x.SagaId);
            });

        migrationBuilder.CreateTable(
            name: "OutboxMessages",
            schema: "Offers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Payload = table.Column<string>(type: "text", nullable: false),
                CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ProcessedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OutboxMessages", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_OfferSagas_CorrelationId",
            schema: "Offers",
            table: "OfferSagas",
            column: "CorrelationId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_OutboxMessages_Type_CorrelationId",
            schema: "Offers",
            table: "OutboxMessages",
            columns: ColumnsArray,
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Offers",
            schema: "Offers");

        migrationBuilder.DropTable(
            name: "OfferSagas",
            schema: "Offers");

        migrationBuilder.DropTable(
            name: "OutboxMessages",
            schema: "Offers");
    }
}
