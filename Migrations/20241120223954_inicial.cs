using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SeuPet.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
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
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 11, 20, 22, 39, 53, 826, DateTimeKind.Utc).AddTicks(6636)),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 11, 20, 22, 39, 53, 826, DateTimeKind.Utc).AddTicks(7023))
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
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 11, 20, 22, 39, 53, 825, DateTimeKind.Utc).AddTicks(9615)),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 11, 20, 22, 39, 53, 825, DateTimeKind.Utc).AddTicks(9866))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adocao",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "integer", nullable: false),
                    AdotanteId = table.Column<int>(type: "integer", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 11, 20, 22, 39, 53, 826, DateTimeKind.Utc).AddTicks(2861)),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 11, 20, 22, 39, 53, 826, DateTimeKind.Utc).AddTicks(3251))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adocao", x => new { x.AdotanteId, x.PetId });
                    table.ForeignKey(
                        name: "FK_Adocao_Adotante_AdotanteId",
                        column: x => x.AdotanteId,
                        principalTable: "Adotante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adocao_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adocao_AdotanteId_PetId",
                table: "Adocao",
                columns: new[] { "AdotanteId", "PetId" });

            migrationBuilder.CreateIndex(
                name: "IX_Adocao_CreateAt",
                table: "Adocao",
                column: "CreateAt");

            migrationBuilder.CreateIndex(
                name: "IX_Adocao_PetId",
                table: "Adocao",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Adotante_Id",
                table: "Adotante",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_Id",
                table: "Pet",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adocao");

            migrationBuilder.DropTable(
                name: "Adotante");

            migrationBuilder.DropTable(
                name: "Pet");
        }
    }
}
