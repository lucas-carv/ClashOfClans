using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class NovaColunaClanTagLigaDeGuerra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "liga-guerra",
                newName: "liga_guerra");

            migrationBuilder.AddColumn<string>(
                name: "clan_tag",
                table: "liga_guerra",
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
                table: "liga_guerra");

            migrationBuilder.RenameTable(
                name: "liga_guerra",
                newName: "liga-guerra");
        }
    }
}
