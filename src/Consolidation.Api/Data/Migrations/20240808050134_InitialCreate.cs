using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consolidation.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consolidations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalRevenue = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TotalExpense = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    FinalBalance = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    DateCreateAt = table.Column<DateOnly>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    HourCreateAt = table.Column<TimeOnly>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    HourUpdateAt = table.Column<TimeOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consolidations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consolidations");
        }
    }
}
