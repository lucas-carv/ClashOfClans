using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class adicionando_referencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_membro_clans_clan_id",
                table: "membro");

            migrationBuilder.DropPrimaryKey(
                name: "pk_clans",
                table: "clans");

            migrationBuilder.RenameTable(
                name: "clans",
                newName: "clan");

            migrationBuilder.RenameColumn(
                name: "ativo",
                table: "membro",
                newName: "situacao");

            migrationBuilder.AlterColumn<int>(
                name: "clan_id",
                table: "membro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_clan",
                table: "clan",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_membro_clans_clan_id",
                table: "membro",
                column: "clan_id",
                principalTable: "clan",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_membro_clans_clan_id",
                table: "membro");

            migrationBuilder.DropPrimaryKey(
                name: "pk_clan",
                table: "clan");

            migrationBuilder.RenameTable(
                name: "clan",
                newName: "clans");

            migrationBuilder.RenameColumn(
                name: "situacao",
                table: "membro",
                newName: "ativo");

            migrationBuilder.AlterColumn<int>(
                name: "clan_id",
                table: "membro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "pk_clans",
                table: "clans",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_membro_clans_clan_id",
                table: "membro",
                column: "clan_id",
                principalTable: "clans",
                principalColumn: "id");
        }
    }
}
