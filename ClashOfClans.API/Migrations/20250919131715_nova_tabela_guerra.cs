using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class nova_tabela_guerra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clan_guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tag = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_alteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clan_guerra", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    inicio_guerra = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fim_guerra = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    clan_id = table.Column<int>(type: "int", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guerra", x => x.id);
                    table.ForeignKey(
                        name: "fk_guerra_clan_guerra_clan_id",
                        column: x => x.clan_id,
                        principalTable: "clan_guerra",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "membro_guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tag = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_guerra_id = table.Column<int>(type: "int", nullable: true),
                    data_alteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membro_guerra", x => x.id);
                    table.ForeignKey(
                        name: "fk_membro_guerra_clan_guerra_clan_guerra_id",
                        column: x => x.clan_guerra_id,
                        principalTable: "clan_guerra",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ataques",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    membro_id = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estrelas = table.Column<int>(type: "int", nullable: false),
                    membro_guerra_id = table.Column<int>(type: "int", nullable: true),
                    data_alteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ataques", x => x.id);
                    table.ForeignKey(
                        name: "fk_ataques_membro_guerra_membro_guerra_id",
                        column: x => x.membro_guerra_id,
                        principalTable: "membro_guerra",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_ataques_membro_guerra_id",
                table: "ataques",
                column: "membro_guerra_id");

            migrationBuilder.CreateIndex(
                name: "ix_guerra_clan_id",
                table: "guerra",
                column: "clan_id");

            migrationBuilder.CreateIndex(
                name: "ix_guerra_inicio_guerra_fim_guerra",
                table: "guerra",
                columns: new[] { "inicio_guerra", "fim_guerra" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_membro_guerra_clan_guerra_id",
                table: "membro_guerra",
                column: "clan_guerra_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ataques");

            migrationBuilder.DropTable(
                name: "guerra");

            migrationBuilder.DropTable(
                name: "membro_guerra");

            migrationBuilder.DropTable(
                name: "clan_guerra");
        }
    }
}
