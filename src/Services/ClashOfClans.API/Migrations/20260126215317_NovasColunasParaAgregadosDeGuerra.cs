using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    public partial class NovasColunasParaAgregadosDeGuerra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ordem_ataque",
                table: "guerra_membro_ataque",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "estrelas",
                table: "clan_em_guerra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "percentual_destruicao",
                table: "clan_em_guerra",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "quantidade_ataques",
                table: "clan_em_guerra",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ordem_ataque",
                table: "guerra_membro_ataque");

            migrationBuilder.DropColumn(
                name: "estrelas",
                table: "clan_em_guerra");

            migrationBuilder.DropColumn(
                name: "percentual_destruicao",
                table: "clan_em_guerra");

            migrationBuilder.DropColumn(
                name: "quantidade_ataques",
                table: "clan_em_guerra");
        }
    }
}
