using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SeuPet.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adotante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sexo = table.Column<int>(type: "integer", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 11, 23, 13, 0, 58, 560, DateTimeKind.Utc).AddTicks(8471)),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 11, 23, 13, 0, 58, 560, DateTimeKind.Utc).AddTicks(8746))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adotante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sexo = table.Column<int>(type: "integer", nullable: false),
                    TipoSanguineo = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Foto = table.Column<string>(type: "text", nullable: false, defaultValue: ""),
                    AdotanteId = table.Column<int>(type: "integer", nullable: true),
                    DataAdocao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 11, 23, 13, 0, 58, 560, DateTimeKind.Utc).AddTicks(7359)),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 11, 23, 13, 0, 58, 560, DateTimeKind.Utc).AddTicks(7647))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pet_Adotante_AdotanteId",
                        column: x => x.AdotanteId,
                        principalTable: "Adotante",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adotante_Id",
                table: "Adotante",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_AdotanteId",
                table: "Pet",
                column: "AdotanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_Id",
                table: "Pet",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "Adotante");
        }
    }
}
