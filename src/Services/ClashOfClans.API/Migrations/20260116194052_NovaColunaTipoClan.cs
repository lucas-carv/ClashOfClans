using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    public partial class NovaColunaTipoClan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clan_em_guerra_guerras_id",
                table: "clan_em_guerra");

            migrationBuilder.DropIndex(
                name: "ix_clan_em_guerra_guerra_id",
                table: "clan_em_guerra");

            migrationBuilder.DropColumn(
                name: "clan_em_guerra_id",
                table: "guerra");

            migrationBuilder.AddColumn<string>(
                name: "tipo",
                table: "clan_em_guerra",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "fk_clan_em_guerra_guerras_guerra_id",
                table: "clan_em_guerra",
                column: "guerra_id",
                principalTable: "guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_clan_em_guerra_guerras_guerra_id",
                table: "clan_em_guerra");

            migrationBuilder.DropColumn(
                name: "tipo",
                table: "clan_em_guerra");

            migrationBuilder.AddColumn<int>(
                name: "clan_em_guerra_id",
                table: "guerra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_clan_em_guerra_guerra_id",
                table: "clan_em_guerra",
                column: "guerra_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_clan_em_guerra_guerras_id",
                table: "clan_em_guerra",
                column: "guerra_id",
                principalTable: "guerra",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
