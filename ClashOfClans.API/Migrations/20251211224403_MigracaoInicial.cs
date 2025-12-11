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
                    tipo_guerra = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    guerra_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                name: "liga_guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    clan_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                name: "membro_inativo_guerra",
                columns: table => new
                {
                    membro_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_avaliacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_entrada_membro = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membro_inativo_guerra", x => new { x.membro_tag, x.clan_tag });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "membros_guerras_resumo",
                columns: table => new
                {
                    clan_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    membro_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    guerras_participadas_seq = table.Column<int>(type: "int", nullable: false),
                    quantidade_ataques = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membros_guerras_resumo", x => new { x.membro_tag, x.clan_tag });
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
                    data_entrada = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    situacao = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membro", x => x.id);
                    table.ForeignKey(
                        name: "fk_membro_clan_clan_id",
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
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
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
                name: "liga_guerra_clan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_level = table.Column<int>(type: "int", nullable: false),
                    liga_de_guerra_id = table.Column<int>(type: "int", nullable: true),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_liga_guerra_clan", x => x.id);
                    table.ForeignKey(
                        name: "fk_liga_guerra_clan_liga_de_guerras_liga_de_guerra_id",
                        column: x => x.liga_de_guerra_id,
                        principalTable: "liga_guerra",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "liga_guerra_rodada",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dia = table.Column<int>(type: "int", nullable: false),
                    inicio_guerra = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    fim_guerra = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    clan_tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_tag_oponente = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    guerra_tag = table.Column<string>(type: "varchar(100)", nullable: false)
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
                        name: "fk_liga_guerra_rodada_liga_de_guerras_liga_de_guerra_id",
                        column: x => x.liga_de_guerra_id,
                        principalTable: "liga_guerra",
                        principalColumn: "id");
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
                    centro_vila_level = table.Column<int>(type: "int", nullable: false),
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
                name: "liga_guerra_membro",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    centro_vila_level = table.Column<int>(type: "int", nullable: false),
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
                    membro_em_guerra_id = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ix_clan_em_guerra_guerra_id_tag",
                table: "clan_em_guerra",
                columns: new[] { "guerra_id", "tag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_guerra_membro_ataque_atacante_tag_defensor_tag_membro_em_gue",
                table: "guerra_membro_ataque",
                columns: new[] { "atacante_tag", "defensor_tag", "membro_em_guerra_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_guerra_membro_ataque_membro_em_guerra_id",
                table: "guerra_membro_ataque",
                column: "membro_em_guerra_id");

            migrationBuilder.CreateIndex(
                name: "ix_liga_guerra_temporada",
                table: "liga_guerra",
                column: "temporada",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guerra_membro_ataque");

            migrationBuilder.DropTable(
                name: "liga_guerra_membro");

            migrationBuilder.DropTable(
                name: "liga_guerra_rodada");

            migrationBuilder.DropTable(
                name: "membro");

            migrationBuilder.DropTable(
                name: "membro_inativo_guerra");

            migrationBuilder.DropTable(
                name: "membros_guerras_resumo");

            migrationBuilder.DropTable(
                name: "membro_em_guerra");

            migrationBuilder.DropTable(
                name: "liga_guerra_clan");

            migrationBuilder.DropTable(
                name: "clan");

            migrationBuilder.DropTable(
                name: "clan_em_guerra");

            migrationBuilder.DropTable(
                name: "liga_guerra");

            migrationBuilder.DropTable(
                name: "guerra");
        }
    }
}
