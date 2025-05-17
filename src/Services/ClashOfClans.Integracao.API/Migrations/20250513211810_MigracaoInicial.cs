using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.Integracao.API.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clan",
                columns: table => new
                {
                    tag = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_level = table.Column<int>(type: "int", nullable: false),
                    attacks = table.Column<int>(type: "int", nullable: false),
                    stars = table.Column<int>(type: "int", nullable: false),
                    destruction_percentage = table.Column<double>(type: "double", nullable: false),
                    exp_earned = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ak_clan_tag", x => x.tag);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "historico_integracao",
                columns: table => new
                {
                    nome_recurso = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    referencia_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_importacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_historico_integracao", x => x.nome_recurso);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "opponent",
                columns: table => new
                {
                    tag = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_level = table.Column<int>(type: "int", nullable: false),
                    stars = table.Column<int>(type: "int", nullable: false),
                    destruction_percentage = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ak_opponent_tag", x => x.tag);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "badge_urls",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    small = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    large = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    medium = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_tag = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    opponent_tag = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_badge_urls", x => x.id);
                    table.ForeignKey(
                        name: "fk_badge_urls_clan_clan_tag",
                        column: x => x.clan_tag,
                        principalTable: "clan",
                        principalColumn: "tag",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_badge_urls_opponent_clan_tag",
                        column: x => x.clan_tag,
                        principalTable: "opponent",
                        principalColumn: "tag",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    result = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    end_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    team_size = table.Column<int>(type: "int", nullable: false),
                    attacks_per_member = table.Column<int>(type: "int", nullable: false),
                    battle_modifier = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    opponent_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_alteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guerra", x => x.id);
                    table.ForeignKey(
                        name: "fk_guerra_clan_clan_id",
                        column: x => x.clan_id,
                        principalTable: "clan",
                        principalColumn: "tag");
                    table.ForeignKey(
                        name: "fk_guerra_opponent_opponent_id",
                        column: x => x.opponent_id,
                        principalTable: "opponent",
                        principalColumn: "tag",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_badge_urls_clan_tag",
                table: "badge_urls",
                column: "clan_tag",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_guerra_clan_id",
                table: "guerra",
                column: "clan_id");

            migrationBuilder.CreateIndex(
                name: "ix_guerra_opponent_id",
                table: "guerra",
                column: "opponent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "badge_urls");

            migrationBuilder.DropTable(
                name: "guerra");

            migrationBuilder.DropTable(
                name: "historico_integracao");

            migrationBuilder.DropTable(
                name: "clan");

            migrationBuilder.DropTable(
                name: "opponent");
        }
    }
}
