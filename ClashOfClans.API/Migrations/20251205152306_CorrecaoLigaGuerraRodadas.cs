using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoLigaGuerraRodadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_liga_guerra_clan_liga_de_guerra_liga_de_guerra_id",
                table: "liga_guerra_clan");

            migrationBuilder.DropForeignKey(
                name: "fk_liga_guerra_rodada_liga_de_guerra_liga_de_guerra_id",
                table: "liga_guerra_rodada");

            migrationBuilder.DropColumn(
                name: "guerra_tags",
                table: "liga_guerra_rodada");

            migrationBuilder.AddForeignKey(
                name: "fk_liga_guerra_clan_liga_de_guerras_liga_de_guerra_id",
                table: "liga_guerra_clan",
                column: "liga_de_guerra_id",
                principalTable: "liga_guerra",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_liga_guerra_rodada_liga_de_guerras_liga_de_guerra_id",
                table: "liga_guerra_rodada",
                column: "liga_de_guerra_id",
                principalTable: "liga_guerra",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_liga_guerra_clan_liga_de_guerras_liga_de_guerra_id",
                table: "liga_guerra_clan");

            migrationBuilder.DropForeignKey(
                name: "fk_liga_guerra_rodada_liga_de_guerras_liga_de_guerra_id",
                table: "liga_guerra_rodada");

            migrationBuilder.AddColumn<string>(
                name: "guerra_tags",
                table: "liga_guerra_rodada",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "fk_liga_guerra_clan_liga_de_guerra_liga_de_guerra_id",
                table: "liga_guerra_clan",
                column: "liga_de_guerra_id",
                principalTable: "liga_guerra",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_liga_guerra_rodada_liga_de_guerra_liga_de_guerra_id",
                table: "liga_guerra_rodada",
                column: "liga_de_guerra_id",
                principalTable: "liga_guerra",
                principalColumn: "id");
        }
    }
}
