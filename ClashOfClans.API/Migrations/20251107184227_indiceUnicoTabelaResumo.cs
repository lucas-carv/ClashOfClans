using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class indiceUnicoTabelaResumo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_membros_guerras_resumo_clan_tag_tag",
                table: "membros_guerras_resumo",
                columns: new[] { "clan_tag", "tag" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_membros_guerras_resumo_clan_tag_tag",
                table: "membros_guerras_resumo");
        }
    }
}
