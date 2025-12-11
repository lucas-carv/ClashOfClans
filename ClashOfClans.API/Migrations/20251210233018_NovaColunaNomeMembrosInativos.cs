using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class NovaColunaNomeMembrosInativos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nome",
                table: "membros_inativos_guerras",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_entrada",
                table: "membro",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldPrecision: 0,
                oldDefaultValue: new DateTime(2025, 12, 10, 20, 24, 58, 969, DateTimeKind.Local).AddTicks(9167));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nome",
                table: "membros_inativos_guerras");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_entrada",
                table: "membro",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(2025, 12, 10, 20, 24, 58, 969, DateTimeKind.Local).AddTicks(9167),
                oldClrType: typeof(DateTime),
                oldType: "datetime(0)",
                oldPrecision: 0);
        }
    }
}
