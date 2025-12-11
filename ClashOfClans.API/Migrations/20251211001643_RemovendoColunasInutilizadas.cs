using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoColunasInutilizadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_membros_inativos_guerras_membros_membro_id",
                table: "membros_inativos_guerras");

            migrationBuilder.DropPrimaryKey(
                name: "pk_membros_inativos_guerras",
                table: "membros_inativos_guerras");

            migrationBuilder.DropIndex(
                name: "ix_membros_inativos_guerras_membro_id",
                table: "membros_inativos_guerras");

            migrationBuilder.DropColumn(
                name: "id",
                table: "membros_inativos_guerras");

            migrationBuilder.DropColumn(
                name: "criado_em",
                table: "membros_inativos_guerras");

            migrationBuilder.DropColumn(
                name: "guerras_analisadas",
                table: "membros_inativos_guerras");

            migrationBuilder.DropColumn(
                name: "guerras_nao_participadas",
                table: "membros_inativos_guerras");

            migrationBuilder.DropColumn(
                name: "guerras_participadas",
                table: "membros_inativos_guerras");

            migrationBuilder.DropColumn(
                name: "membro_id",
                table: "membros_inativos_guerras");

            migrationBuilder.DropColumn(
                name: "motivo",
                table: "membros_inativos_guerras");

            migrationBuilder.RenameTable(
                name: "membros_inativos_guerras",
                newName: "membro_inativo_guerra");

            migrationBuilder.AddColumn<string>(
                name: "membro_tag",
                table: "membro_inativo_guerra",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "pk_membro_inativo_guerra",
                table: "membro_inativo_guerra",
                columns: new[] { "membro_tag", "clan_tag" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_membro_inativo_guerra",
                table: "membro_inativo_guerra");

            migrationBuilder.DropColumn(
                name: "membro_tag",
                table: "membro_inativo_guerra");

            migrationBuilder.RenameTable(
                name: "membro_inativo_guerra",
                newName: "membros_inativos_guerras");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "membros_inativos_guerras",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "criado_em",
                table: "membros_inativos_guerras",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "guerras_analisadas",
                table: "membros_inativos_guerras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "guerras_nao_participadas",
                table: "membros_inativos_guerras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "guerras_participadas",
                table: "membros_inativos_guerras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "membro_id",
                table: "membros_inativos_guerras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "motivo",
                table: "membros_inativos_guerras",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "pk_membros_inativos_guerras",
                table: "membros_inativos_guerras",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_membros_inativos_guerras_membro_id",
                table: "membros_inativos_guerras",
                column: "membro_id");

            migrationBuilder.AddForeignKey(
                name: "fk_membros_inativos_guerras_membros_membro_id",
                table: "membros_inativos_guerras",
                column: "membro_id",
                principalTable: "membro",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
