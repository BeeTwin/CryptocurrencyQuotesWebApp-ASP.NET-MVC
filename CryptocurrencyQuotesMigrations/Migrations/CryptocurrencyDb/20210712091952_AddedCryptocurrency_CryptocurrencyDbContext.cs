using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptocurrencyQuotesMigrations.Migrations.CryptocurrencyDb
{
    public partial class AddedCryptocurrency_CryptocurrencyDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cryptocurrency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(30,15)", nullable: false),
                    PercentChange1h = table.Column<decimal>(type: "decimal(30,15)", nullable: false),
                    PercentChange24h = table.Column<decimal>(type: "decimal(30,15)", nullable: false),
                    MarketCap = table.Column<decimal>(type: "decimal(30,15)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptocurrency", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cryptocurrency");
        }
    }
}
