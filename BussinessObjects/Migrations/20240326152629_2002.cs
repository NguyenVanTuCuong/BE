using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class _2002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    OrchidId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Orchid",
                        column: x => x.OrchidId,
                        principalTable: "Orchid",
                        principalColumn: "OrchidId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OrchidId",
                table: "Transaction",
                column: "OrchidId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
