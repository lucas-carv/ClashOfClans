using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class NovaColunaTipoGuerra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_guerra_inicio_guerra_fim_guerra",
                table: "guerra");

            migrationBuilder.AddColumn<string>(
                name: "tipo_guerra",
                table: "guerra",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipo_guerra",
                table: "guerra");

            migrationBuilder.CreateIndex(
                name: "ix_guerra_inicio_guerra_fim_guerra",
                table: "guerra",
                columns: new[] { "inicio_guerra", "fim_guerra" },
                unique: true);
        }
    }
}
