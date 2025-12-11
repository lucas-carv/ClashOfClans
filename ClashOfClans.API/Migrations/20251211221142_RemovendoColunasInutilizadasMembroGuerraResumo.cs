using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoColunasInutilizadasMembroGuerraResumo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_membros_guerras_resumo",
                table: "membros_guerras_resumo");

            migrationBuilder.DropIndex(
                name: "ix_membros_guerras_resumo_clan_tag_tag",
                table: "membros_guerras_resumo");

            migrationBuilder.DropColumn(
                name: "id",
                table: "membros_guerras_resumo");

            migrationBuilder.DropColumn(
                name: "data_alteracao",
                table: "membros_guerras_resumo");

            migrationBuilder.DropColumn(
                name: "data_criacao",
                table: "membros_guerras_resumo");

            migrationBuilder.DropColumn(
                name: "foi_removido",
                table: "membros_guerras_resumo");

            migrationBuilder.RenameColumn(
                name: "tag",
                table: "membros_guerras_resumo",
                newName: "membro_tag");

            migrationBuilder.AddPrimaryKey(
                name: "pk_membros_guerras_resumo",
                table: "membros_guerras_resumo",
                columns: new[] { "membro_tag", "clan_tag" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_membros_guerras_resumo",
                table: "membros_guerras_resumo");

            migrationBuilder.RenameColumn(
                name: "membro_tag",
                table: "membros_guerras_resumo",
                newName: "tag");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "membros_guerras_resumo",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_alteracao",
                table: "membros_guerras_resumo",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "data_criacao",
                table: "membros_guerras_resumo",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "foi_removido",
                table: "membros_guerras_resumo",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_membros_guerras_resumo",
                table: "membros_guerras_resumo",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_membros_guerras_resumo_clan_tag_tag",
                table: "membros_guerras_resumo",
                columns: new[] { "clan_tag", "tag" },
                unique: true);
        }
    }
}
