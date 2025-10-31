using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class removendo_tag_inutilizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "guerra_clan_id",
                table: "membro_em_guerra");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "guerra_clan_id",
                table: "membro_em_guerra",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
