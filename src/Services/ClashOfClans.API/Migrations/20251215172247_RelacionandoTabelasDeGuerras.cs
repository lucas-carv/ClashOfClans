using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class RelacionandoTabelasDeGuerras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clan_em_guerra_guerras_guerra_id",
                table: "clan_em_guerra");

            migrationBuilder.DropForeignKey(
                name: "fk_guerra_membro_ataque_membro_em_guerra_membro_em_guerra_id",
                table: "guerra_membro_ataque");

            migrationBuilder.DropForeignKey(
                name: "fk_membro_em_guerra_clan_em_guerra_clan_em_guerra_id",
                table: "membro_em_guerra");

            migrationBuilder.AlterColumn<int>(
                name: "clan_em_guerra_id",
                table: "membro_em_guerra",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "clan_em_guerra_id",
                table: "guerra",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "fk_membro_em_guerra_clan_em_guerra_clan_em_guerra_id",
                table: "membro_em_guerra",
                column: "clan_em_guerra_id",
                principalTable: "clan_em_guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clan_em_guerra_guerras_guerra_id",
                table: "clan_em_guerra");

            migrationBuilder.DropForeignKey(
                name: "fk_guerra_membro_ataque_membros_em_guerra_membro_em_guerra_id",
                table: "guerra_membro_ataque");

            migrationBuilder.DropForeignKey(
                name: "fk_membro_em_guerra_clan_em_guerra_clan_em_guerra_id",
                table: "membro_em_guerra");

            migrationBuilder.DropColumn(
                name: "clan_em_guerra_id",
                table: "guerra");

            migrationBuilder.AlterColumn<int>(
                name: "clan_em_guerra_id",
                table: "membro_em_guerra",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_clan_em_guerra_guerras_guerra_id",
                table: "clan_em_guerra",
                column: "guerra_id",
                principalTable: "guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_membro_ataque_membro_em_guerra_membro_em_guerra_id",
                table: "guerra_membro_ataque",
                column: "membro_em_guerra_id",
                principalTable: "membro_em_guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_membro_em_guerra_clan_em_guerra_clan_em_guerra_id",
                table: "membro_em_guerra",
                column: "clan_em_guerra_id",
                principalTable: "clan_em_guerra",
                principalColumn: "id");
        }
    }
}
