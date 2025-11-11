using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
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
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clan", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    status = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    inicio_guerra = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    fim_guerra = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guerra", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "membros_guerras_resumo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    clan_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    guerras_participadas_seq = table.Column<int>(type: "int", nullable: false),
                    quantidade_ataques = table.Column<int>(type: "int", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membros_guerras_resumo", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "membro",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    clan_id = table.Column<int>(type: "int", nullable: false),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    situacao = table.Column<int>(type: "int", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membro", x => x.id);
                    table.ForeignKey(
                        name: "fk_membro_clans_clan_id",
                        column: x => x.clan_id,
                        principalTable: "clan",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clan_em_guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    guerra_id = table.Column<int>(type: "int", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clan_em_guerra", x => x.id);
                    table.ForeignKey(
                        name: "fk_clan_em_guerra_guerras_guerra_id",
                        column: x => x.guerra_id,
                        principalTable: "guerra",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "membro_em_guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_em_guerra_id = table.Column<int>(type: "int", nullable: true),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membro_em_guerra", x => x.id);
                    table.ForeignKey(
                        name: "fk_membro_em_guerra_clan_em_guerra_clan_em_guerra_id",
                        column: x => x.clan_em_guerra_id,
                        principalTable: "clan_em_guerra",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "guerra_membro_ataque",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    atacante_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    defensor_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estrelas = table.Column<int>(type: "int", nullable: false),
                    membro_em_guerra_id = table.Column<int>(type: "int", nullable: true),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guerra_membro_ataque", x => x.id);
                    table.ForeignKey(
                        name: "fk_guerra_membro_ataque_membro_em_guerra_membro_em_guerra_id",
                        column: x => x.membro_em_guerra_id,
                        principalTable: "membro_em_guerra",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_clan_tag",
                table: "clan",
                column: "tag",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clan_em_guerra_guerra_id",
                table: "clan_em_guerra",
                column: "guerra_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_guerra_inicio_guerra_fim_guerra",
                table: "guerra",
                columns: new[] { "inicio_guerra", "fim_guerra" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_guerra_membro_ataque_membro_em_guerra_id",
                table: "guerra_membro_ataque",
                column: "membro_em_guerra_id");

            migrationBuilder.CreateIndex(
                name: "ix_membro_clan_id",
                table: "membro",
                column: "clan_id");

            migrationBuilder.CreateIndex(
                name: "ix_membro_tag",
                table: "membro",
                column: "tag",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_membro_em_guerra_clan_em_guerra_id",
                table: "membro_em_guerra",
                column: "clan_em_guerra_id");

            migrationBuilder.CreateIndex(
                name: "ix_membros_guerras_resumo_clan_tag_tag",
                table: "membros_guerras_resumo",
                columns: new[] { "clan_tag", "tag" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guerra_membro_ataque");

            migrationBuilder.DropTable(
                name: "membro");

            migrationBuilder.DropTable(
                name: "membros_guerras_resumo");

            migrationBuilder.DropTable(
                name: "membro_em_guerra");

            migrationBuilder.DropTable(
                name: "clan");

            migrationBuilder.DropTable(
                name: "clan_em_guerra");

            migrationBuilder.DropTable(
                name: "guerra");
        }
    }
}
