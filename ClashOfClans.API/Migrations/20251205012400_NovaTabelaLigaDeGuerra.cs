using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class NovaTabelaLigaDeGuerra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_membro_clans_clan_id",
                table: "membro");

            migrationBuilder.CreateTable(
                name: "liga-guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    temporada = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_liga_guerra", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "liga_guerra_clan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_level = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    liga_de_guerra_id = table.Column<int>(type: "int", nullable: true),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_liga_guerra_clan", x => x.id);
                    table.ForeignKey(
                        name: "fk_liga_guerra_clan_liga_de_guerra_liga_de_guerra_id",
                        column: x => x.liga_de_guerra_id,
                        principalTable: "liga-guerra",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "liga_guerra_rodada",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    guerra_tags = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    liga_de_guerra_id = table.Column<int>(type: "int", nullable: true),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_liga_guerra_rodada", x => x.id);
                    table.ForeignKey(
                        name: "fk_liga_guerra_rodada_liga_de_guerra_liga_de_guerra_id",
                        column: x => x.liga_de_guerra_id,
                        principalTable: "liga-guerra",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "liga_guerra_membro",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    centro_vila_level = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    liga_guerra_clan_id = table.Column<int>(type: "int", nullable: true),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_liga_guerra_membro", x => x.id);
                    table.ForeignKey(
                        name: "fk_liga_guerra_membro_liga_guerra_clan_liga_guerra_clan_id",
                        column: x => x.liga_guerra_clan_id,
                        principalTable: "liga_guerra_clan",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_liga_guerra_clan_liga_de_guerra_id",
                table: "liga_guerra_clan",
                column: "liga_de_guerra_id");

            migrationBuilder.CreateIndex(
                name: "ix_liga_guerra_membro_liga_guerra_clan_id",
                table: "liga_guerra_membro",
                column: "liga_guerra_clan_id");

            migrationBuilder.CreateIndex(
                name: "ix_liga_guerra_rodada_liga_de_guerra_id",
                table: "liga_guerra_rodada",
                column: "liga_de_guerra_id");

            migrationBuilder.CreateIndex(
                name: "ix_liga_guerra_temporada",
                table: "liga-guerra",
                column: "temporada",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_membro_clan_clan_id",
                table: "membro",
                column: "clan_id",
                principalTable: "clan",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_membro_clan_clan_id",
                table: "membro");

            migrationBuilder.DropTable(
                name: "liga_guerra_membro");

            migrationBuilder.DropTable(
                name: "liga_guerra_rodada");

            migrationBuilder.DropTable(
                name: "liga_guerra_clan");

            migrationBuilder.DropTable(
                name: "liga-guerra");

            migrationBuilder.AddForeignKey(
                name: "fk_membro_clans_clan_id",
                table: "membro",
                column: "clan_id",
                principalTable: "clan",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
