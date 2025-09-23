using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class membros_geurra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guerra_clan_membro_guerra_clan_guerra_clan_id",
                table: "guerra_clan_membro");

            migrationBuilder.AlterColumn<int>(
                name: "guerra_clan_id",
                table: "guerra_clan_membro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_clan_membro_guerra_clan_guerra_clan_id",
                table: "guerra_clan_membro",
                column: "guerra_clan_id",
                principalTable: "guerra_clan",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guerra_clan_membro_guerra_clan_guerra_clan_id",
                table: "guerra_clan_membro");

            migrationBuilder.AlterColumn<int>(
                name: "guerra_clan_id",
                table: "guerra_clan_membro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_clan_membro_guerra_clan_guerra_clan_id",
                table: "guerra_clan_membro",
                column: "guerra_clan_id",
                principalTable: "guerra_clan",
                principalColumn: "id");
        }
    }
}
