using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    public partial class NovasColunasPosicaoNoMapaEPercentualDestruicao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clan_em_guerra_guerras_guerra_id",
                table: "clan_em_guerra");

            migrationBuilder.DropForeignKey(
                name: "fk_guerra_membro_ataque_membros_em_guerra_membro_em_guerra_id",
                table: "guerra_membro_ataque");

            migrationBuilder.AddColumn<int>(
                name: "posicao_mapa",
                table: "membro_em_guerra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "percentual_destruicao",
                table: "guerra_membro_ataque",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "fk_clan_em_guerra_guerras_id",
                table: "clan_em_guerra",
                column: "guerra_id",
                principalTable: "guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_membro_ataque_membros_em_guerra_membro_em_guerra_id1",
                table: "guerra_membro_ataque",
                column: "membro_em_guerra_id",
                principalTable: "membro_em_guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clan_em_guerra_guerras_id",
                table: "clan_em_guerra");

            migrationBuilder.DropForeignKey(
                name: "fk_guerra_membro_ataque_membros_em_guerra_membro_em_guerra_id1",
                table: "guerra_membro_ataque");

            migrationBuilder.DropColumn(
                name: "posicao_mapa",
                table: "membro_em_guerra");

            migrationBuilder.DropColumn(
                name: "percentual_destruicao",
                table: "guerra_membro_ataque");

            migrationBuilder.AddForeignKey(
                name: "fk_clan_em_guerra_guerras_guerra_id",
                table: "clan_em_guerra",
                column: "guerra_id",
                principalTable: "guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_membro_ataque_membros_em_guerra_membro_em_guerra_id",
                table: "guerra_membro_ataque",
                column: "membro_em_guerra_id",
                principalTable: "membro_em_guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
