using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoTabelaRodadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "clan_tag",
                table: "liga_guerra_rodada",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "clan_tag_oponente",
                table: "liga_guerra_rodada",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "dia",
                table: "liga_guerra_rodada",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "guerra_tag",
                table: "liga_guerra_rodada",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clan_tag",
                table: "liga_guerra_rodada");

            migrationBuilder.DropColumn(
                name: "clan_tag_oponente",
                table: "liga_guerra_rodada");

            migrationBuilder.DropColumn(
                name: "dia",
                table: "liga_guerra_rodada");

            migrationBuilder.DropColumn(
                name: "guerra_tag",
                table: "liga_guerra_rodada");
        }
    }
}
