using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class NovosIndicesUnicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guerra_membro_ataque_membro_em_guerra_membro_em_guerra_id",
                table: "guerra_membro_ataque");

            migrationBuilder.DropIndex(
                name: "ix_guerra_membro_ataque_atacante_tag_defensor_tag",
                table: "guerra_membro_ataque");

            migrationBuilder.AlterColumn<int>(
                name: "membro_em_guerra_id",
                table: "guerra_membro_ataque",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_guerra_membro_ataque_atacante_tag_defensor_tag_membro_em_gue",
                table: "guerra_membro_ataque",
                columns: new[] { "atacante_tag", "defensor_tag", "membro_em_guerra_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clan_em_guerra_guerra_id_tag",
                table: "clan_em_guerra",
                columns: new[] { "guerra_id", "tag" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_membro_ataque_membro_em_guerra_membro_em_guerra_id",
                table: "guerra_membro_ataque",
                column: "membro_em_guerra_id",
                principalTable: "membro_em_guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guerra_membro_ataque_membro_em_guerra_membro_em_guerra_id",
                table: "guerra_membro_ataque");

            migrationBuilder.DropIndex(
                name: "ix_guerra_membro_ataque_atacante_tag_defensor_tag_membro_em_gue",
                table: "guerra_membro_ataque");

            migrationBuilder.DropIndex(
                name: "ix_clan_em_guerra_guerra_id_tag",
                table: "clan_em_guerra");

            migrationBuilder.AlterColumn<int>(
                name: "membro_em_guerra_id",
                table: "guerra_membro_ataque",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "ix_guerra_membro_ataque_atacante_tag_defensor_tag",
                table: "guerra_membro_ataque",
                columns: new[] { "atacante_tag", "defensor_tag" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_membro_ataque_membro_em_guerra_membro_em_guerra_id",
                table: "guerra_membro_ataque",
                column: "membro_em_guerra_id",
                principalTable: "membro_em_guerra",
                principalColumn: "id");
        }
    }
}
