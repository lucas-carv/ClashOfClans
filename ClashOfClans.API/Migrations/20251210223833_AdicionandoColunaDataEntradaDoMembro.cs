using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClashOfClans.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoColunaDataEntradaDoMembro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "data_entrada",
                table: "membro",
                type: "datetime(0)",
                precision: 0,
                nullable: false,
                defaultValue: new DateTime(2025, 12, 10, 19, 38, 32, 689, DateTimeKind.Local).AddTicks(7911));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_entrada",
                table: "membro");
        }
    }
}
