using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    public partial class NovaTabelaLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "log_guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    resultado = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fim_guerra = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    quantidade_membros = table.Column<int>(type: "int", nullable: false),
                    ataques_por_membro = table.Column<int>(type: "int", nullable: false),
                    modificador_de_batalha = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_log_guerra", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "log_clan_guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    log_guerra_id = table.Column<int>(type: "int", nullable: false),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_level = table.Column<int>(type: "int", nullable: false),
                    quantidade_ataques = table.Column<int>(type: "int", nullable: false),
                    estrelas = table.Column<int>(type: "int", nullable: false),
                    porcentagem_destruicao = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    exp_ganho = table.Column<int>(type: "int", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_log_clan_guerra", x => x.id);
                    table.ForeignKey(
                        name: "fk_log_clan_guerra_logs_guerras_log_guerra_id",
                        column: x => x.log_guerra_id,
                        principalTable: "log_guerra",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "log_oponente_guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    log_guerra_id = table.Column<int>(type: "int", nullable: false),
                    tag = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    clan_level = table.Column<int>(type: "int", nullable: false),
                    estrelas = table.Column<int>(type: "int", nullable: false),
                    porcentagem_destruicao = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(0)", precision: 0, nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_log_oponente_guerra", x => x.id);
                    table.ForeignKey(
                        name: "fk_log_oponente_guerra_logs_guerras_log_guerra_id",
                        column: x => x.log_guerra_id,
                        principalTable: "log_guerra",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_log_clan_guerra_log_guerra_id",
                table: "log_clan_guerra",
                column: "log_guerra_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_log_oponente_guerra_log_guerra_id",
                table: "log_oponente_guerra",
                column: "log_guerra_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "log_clan_guerra");

            migrationBuilder.DropTable(
                name: "log_oponente_guerra");

            migrationBuilder.DropTable(
                name: "log_guerra");
        }
    }
}
