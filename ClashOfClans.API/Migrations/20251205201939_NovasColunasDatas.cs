using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class NovasColunasDatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "centro_vila_level",
                table: "membro_em_guerra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "fim_guerra",
                table: "liga_guerra_rodada",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "inicio_guerra",
                table: "liga_guerra_rodada",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "liga_guerra_rodada",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "nome",
                table: "clan_em_guerra",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "centro_vila_level",
                table: "membro_em_guerra");

            migrationBuilder.DropColumn(
                name: "fim_guerra",
                table: "liga_guerra_rodada");

            migrationBuilder.DropColumn(
                name: "inicio_guerra",
                table: "liga_guerra_rodada");

            migrationBuilder.DropColumn(
                name: "status",
                table: "liga_guerra_rodada");

            migrationBuilder.DropColumn(
                name: "nome",
                table: "clan_em_guerra");
        }
    }
}
