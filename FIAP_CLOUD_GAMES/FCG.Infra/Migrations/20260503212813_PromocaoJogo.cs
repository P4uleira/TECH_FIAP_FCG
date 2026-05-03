using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Infra.Migrations
{
    /// <inheritdoc />
    public partial class PromocaoJogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecoPromocional",
                table: "Jogos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PromocaoExpiracao",
                table: "Jogos",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoPromocional",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "PromocaoExpiracao",
                table: "Jogos");
        }
    }
}
