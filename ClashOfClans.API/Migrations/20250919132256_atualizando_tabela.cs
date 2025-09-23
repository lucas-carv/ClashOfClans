using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class atualizando_tabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guerra_clan_guerra_clan_id",
                table: "guerra");

            migrationBuilder.DropForeignKey(
                name: "fk_membro_guerra_clan_guerra_clan_guerra_id",
                table: "membro_guerra");

            migrationBuilder.DropTable(
                name: "ataques");

            migrationBuilder.DropTable(
                name: "clan_guerra");

            migrationBuilder.DropPrimaryKey(
                name: "pk_membro_guerra",
                table: "membro_guerra");

            migrationBuilder.RenameTable(
                name: "membro_guerra",
                newName: "guerra_clan_membro");

            migrationBuilder.RenameColumn(
                name: "clan_guerra_id",
                table: "guerra_clan_membro",
                newName: "guerra_clan_id");

            migrationBuilder.RenameIndex(
                name: "ix_membro_guerra_clan_guerra_id",
                table: "guerra_clan_membro",
                newName: "ix_guerra_clan_membro_guerra_clan_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_guerra_clan_membro",
                table: "guerra_clan_membro",
                column: "id");

            migrationBuilder.CreateTable(
                name: "guerra_clan",
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
                    table.PrimaryKey("pk_guerra_clan", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "guerra_membro_ataque",
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
                    table.PrimaryKey("pk_guerra_membro_ataque", x => x.id);
                    table.ForeignKey(
                        name: "fk_guerra_membro_ataque_membro_guerra_membro_guerra_id",
                        column: x => x.membro_guerra_id,
                        principalTable: "guerra_clan_membro",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_guerra_membro_ataque_membro_guerra_id",
                table: "guerra_membro_ataque",
                column: "membro_guerra_id");

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_guerra_clan_clan_id",
                table: "guerra",
                column: "clan_id",
                principalTable: "guerra_clan",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_clan_membro_guerra_clan_guerra_clan_id",
                table: "guerra_clan_membro",
                column: "guerra_clan_id",
                principalTable: "guerra_clan",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guerra_guerra_clan_clan_id",
                table: "guerra");

            migrationBuilder.DropForeignKey(
                name: "fk_guerra_clan_membro_guerra_clan_guerra_clan_id",
                table: "guerra_clan_membro");

            migrationBuilder.DropTable(
                name: "guerra_clan");

            migrationBuilder.DropTable(
                name: "guerra_membro_ataque");

            migrationBuilder.DropPrimaryKey(
                name: "pk_guerra_clan_membro",
                table: "guerra_clan_membro");

            migrationBuilder.RenameTable(
                name: "guerra_clan_membro",
                newName: "membro_guerra");

            migrationBuilder.RenameColumn(
                name: "guerra_clan_id",
                table: "membro_guerra",
                newName: "clan_guerra_id");

            migrationBuilder.RenameIndex(
                name: "ix_guerra_clan_membro_guerra_clan_id",
                table: "membro_guerra",
                newName: "ix_membro_guerra_clan_guerra_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_membro_guerra",
                table: "membro_guerra",
                column: "id");

            migrationBuilder.CreateTable(
                name: "ataques",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    data_alteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estrelas = table.Column<int>(type: "int", nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    membro_guerra_id = table.Column<int>(type: "int", nullable: true),
                    membro_id = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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

            migrationBuilder.CreateTable(
                name: "clan_guerra",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    data_alteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    foi_removido = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    tag = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clan_guerra", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_ataques_membro_guerra_id",
                table: "ataques",
                column: "membro_guerra_id");

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_clan_guerra_clan_id",
                table: "guerra",
                column: "clan_id",
                principalTable: "clan_guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_membro_guerra_clan_guerra_clan_guerra_id",
                table: "membro_guerra",
                column: "clan_guerra_id",
                principalTable: "clan_guerra",
                principalColumn: "id");
        }
    }
}
