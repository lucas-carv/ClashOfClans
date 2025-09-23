using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class atualizando_tabela_guerra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guerra_guerra_clan_clan_id",
                table: "guerra");

            migrationBuilder.DropIndex(
                name: "ix_guerra_clan_id",
                table: "guerra");

            migrationBuilder.DropColumn(
                name: "clan_id",
                table: "guerra");

            migrationBuilder.AddColumn<int>(
                name: "guerra_id",
                table: "guerra_clan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_guerra_clan_guerra_id",
                table: "guerra_clan",
                column: "guerra_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_clan_guerra_guerra_id",
                table: "guerra_clan",
                column: "guerra_id",
                principalTable: "guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_guerra_clan_guerra_guerra_id",
                table: "guerra_clan");

            migrationBuilder.DropIndex(
                name: "ix_guerra_clan_guerra_id",
                table: "guerra_clan");

            migrationBuilder.DropColumn(
                name: "guerra_id",
                table: "guerra_clan");

            migrationBuilder.AddColumn<int>(
                name: "clan_id",
                table: "guerra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_guerra_clan_id",
                table: "guerra",
                column: "clan_id");

            migrationBuilder.AddForeignKey(
                name: "fk_guerra_guerra_clan_clan_id",
                table: "guerra",
                column: "clan_id",
                principalTable: "guerra_clan",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
