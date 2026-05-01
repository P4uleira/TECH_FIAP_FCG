using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FCG.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AuthEBiblioteca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioJogos");

            migrationBuilder.AddColumn<string>(
                name: "AcessoDescricao",
                table: "Acessos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Biblioteca",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biblioteca", x => new { x.UsuarioId, x.JogoId });
                    table.ForeignKey(
                        name: "FK_Biblioteca_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Biblioteca_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Acessos",
                columns: new[] { "Id", "AcessoDescricao", "AcessoNome" },
                values: new object[,]
                {
                    { new Guid("0c6cd9e6-f3ea-44a0-9c4d-ecd7906089de"), "Acesso padrão para usuários", "Usuario" },
                    { new Guid("8d87caaf-6345-4865-9057-45a8b6b5d882"), "Acesso completo para administradores", "Administrador" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biblioteca_JogoId",
                table: "Biblioteca",
                column: "JogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biblioteca");

            migrationBuilder.DeleteData(
                table: "Acessos",
                keyColumn: "Id",
                keyValue: new Guid("0c6cd9e6-f3ea-44a0-9c4d-ecd7906089de"));

            migrationBuilder.DeleteData(
                table: "Acessos",
                keyColumn: "Id",
                keyValue: new Guid("8d87caaf-6345-4865-9057-45a8b6b5d882"));

            migrationBuilder.DropColumn(
                name: "AcessoDescricao",
                table: "Acessos");

            migrationBuilder.CreateTable(
                name: "UsuarioJogos",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioJogos", x => new { x.UsuarioId, x.JogoId });
                    table.ForeignKey(
                        name: "FK_UsuarioJogos_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioJogos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioJogos_JogoId",
                table: "UsuarioJogos",
                column: "JogoId");
        }
    }
}
